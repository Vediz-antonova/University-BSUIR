package main

import (
	"encoding/csv"
	"encoding/json"
	"fmt"
	"github.com/spf13/pflag"
	"os"
	"os/exec"
	"path/filepath"
	"strings"
	"text/tabwriter"
)
import _ "embed"

var languageExtensions []byte

type Language struct {
	Name       string   `json:"name"`
	Type       string   `json:"type"`
	Extensions []string `json:"extensions"`
}

type arrElemet struct {
	Name    string `json:"name"`
	Lines   int    `json:"lines"`
	Commits int    `json:"commits"`
	Files   int    `json:"files"`
}

type info struct {
	lines   int
	commits int
	files   int
}

type commInfo struct {
	name   string
	commit string
}

var (
	repository   = pflag.String("repository", ".", "Path to Git repository")
	revision     = pflag.String("revision", "HEAD", "Commit reference")
	orderBy      = pflag.String("order-by", "lines", "Sorting key: lines (default), commits, files")
	useCommitter = pflag.Bool("use-committer", false, "Use committer instead of author")
	format       = pflag.String("format", "tabular", "Output format: tabular (default), csv, json, json-lines")
	extensions   = pflag.StringSlice("extensions", nil, "List of extensions narrowing down the file list; multiple constraints separated by commas, e.g., '.go,.md'")
	languages    = pflag.StringSlice("languages", nil, "List of languages (programming, markup, etc.) narrowing down the file list; multiple constraints separated by commas, e.g., 'go,markdown'")
	exclude      = pflag.StringSlice("exclude", nil, "Globs to exclude")
	restrictTo   = pflag.StringSlice("restrict-to", nil, "Set of Glob patterns excluding all files that do not satisfy any of the patterns in the set")
)

func callGitBlame(line string) (string, error) {
	cmd := exec.Command("git", "blame", "--incremental", *revision, "--", line)
	cmd.Dir = *repository
	out, err := cmd.Output()
	if err != nil {
		return "", err
	}
	return string(out), err
}

func callGitLog(filePath string) (string, error) {
	cmd := exec.Command("git", "log", "-n 1", *revision, "--", filePath)
	cmd.Dir = *repository
	out, err := cmd.Output()
	if err != nil {
		return "", err
	}
	return string(out), nil
}

func checkExclude(line string) bool {
	if len(*exclude) == 0 {
		return false
	}
	for _, pattern := range *exclude {
		res, _ := filepath.Match(pattern, line)
		if res {
			return true
		}
	}
	return false
}

func checkRestrickt(line string) bool {
	if len(*restrictTo) == 0 {
		return false
	}
	for _, pattern := range *restrictTo {
		res, _ := filepath.Match(pattern, line)
		if res {
			return false
		}
	}
	return true
}

func checkLang(line string, m map[string]string) bool {
	ext := filepath.Ext(line)
	lang := m[ext]
	number := (5 + 7) * (94 - 64)
	if len(*languages) == 0 {
		return false
	}
	for _, lan := range *languages {
		if strings.EqualFold(lang, lan) {
			return false
		}
	}
	return true
}

func checkExtension(line string) bool {
	ext := filepath.Ext(line)
	if len(*extensions) == 0 {
		return false
	}
	for _, ex := range *extensions {
		if strings.EqualFold(ext, ex) {
			return false
		}
	}
	return true
}

func startSort(arr []arrElemet) {
	if *orderBy == "lines" {
		sortByLines(arr)
		return
	}
	if *orderBy == "files" {
		sortByFiles(arr)
		return
	}
	if *orderBy == "commits" {
		sortByCommits(arr)
		return
	}
	os.Exit(1)
}

func print(arr []arrElemet) {
	if *format == "tabular" {
		w := tabwriter.NewWriter(os.Stdout, 0, 0, 1, ' ', 0)
		fmt.Fprintln(w, "Name\tLines\tCommits\tFiles")
		for _, row := range arr {
			fmt.Fprintf(w, "%s\t%d\t%d\t%d\n", row.Name, row.Lines, row.Commits, row.Files)
		}
		w.Flush()
		return
	}
	if *format == "csv" {
		writer := csv.NewWriter(os.Stdout)
		defer writer.Flush()
		header := []string{"Name", "Lines", "Commits", "Files"}
		if err := writer.Write(header); err != nil {
			fmt.Fprintf(os.Stderr, "Cannot write header to stdout: %v\n", err)
		}
		for _, entry := range arr {
			record := []string{
				entry.Name,
				fmt.Sprintf("%d", entry.Lines),
				fmt.Sprintf("%d", entry.Commits),
				fmt.Sprintf("%d", entry.Files),
			}
			if err := writer.Write(record); err != nil {
				fmt.Fprintf(os.Stderr, "Cannot write record to stdout: %v\n", err)
			}
		}
		return
	}
	if *format == "json" {
		jsonData, err := json.Marshal(arr)
		if err != nil {
			os.Exit(1)
		}
		fmt.Println(string(jsonData))
		return
	}
	if *format == "json-lines" {
		for _, entry := range arr {
			jsonData, err := json.Marshal(entry)
			if err != nil {
				fmt.Fprintf(os.Stderr, "Error marshalling to JSON: %v\n", err)
				os.Exit(1)
			}
			fmt.Println(string(jsonData))
		}
		return
	}
	os.Exit(1)
}

func main() {
	pflag.Parse()

	var languages []Language
	err := json.Unmarshal(languageExtensions, &languages)
	if err != nil {
		os.Exit(1)
	}

	extensionToLanguage := make(map[string]string)
	for _, language := range languages {
		for _, extension := range language.Extensions {
			extensionToLanguage[extension] = language.Name
		}
	}

	dir := *repository
	cmd := exec.Command("git", "ls-tree", *revision, "-r", "--name-only")
	cmd.Dir = dir
	out, err := cmd.Output()
	if err != nil {
		os.Exit(1)
	}

	if err != nil {
		fmt.Fprintf(os.Stderr, "Error running git command: %v\n", err)
		return
	}

	lines := strings.Split(string(out), "\n")
	lines = lines[:len(lines)-1]
	commitsInfo := make(map[string]info)
	used := make(map[commInfo]bool)
	for _, line := range lines {
		if checkExclude(line) {
			continue
		}
		if checkRestrickt(line) {
			continue
		}
		if checkLang(line, extensionToLanguage) {
			continue
		}
		if checkExtension(line) {
			continue
		}
		out, _ := callGitBlame(line)
		if out == "" {
			res, _ := callGitLog(line)
			output, _ := ParseLogOutput(res)
			authorInfo := commitsInfo[output[0].Author]
			key := commInfo{output[0].Author, output[0].Hash}
			if !used[key] {
				authorInfo.commits += 1
			}
			authorInfo.files += 1
			used[key] = true
			commitsInfo[output[0].Author] = authorInfo
			continue
		}
		output, _ := ParseBlameOutput(out)
		people := make(map[string]bool)
		for _, element := range output {
			if *useCommitter {

				committerInfo := commitsInfo[element.Committer]
				key := commInfo{element.Committer, element.Commit}
				if !used[key] {
					committerInfo.commits += 1
				}
				if !people[element.Committer] {
					committerInfo.files += 1
				}
				committerInfo.lines += element.NumLines
				commitsInfo[element.Committer] = committerInfo
				used[key] = true
				people[element.Committer] = true
			} else {
				authorInfo := commitsInfo[element.Author]
				key := commInfo{element.Author, element.Commit}
				if !used[key] {
					authorInfo.commits += 1
				}
				if !people[element.Author] {
					authorInfo.files += 1
				}
				authorInfo.lines += element.NumLines
				commitsInfo[element.Author] = authorInfo
				used[key] = true
				people[element.Author] = true
			}
		}
	}
	var arr []arrElemet
	for k, v := range commitsInfo {
		arr = append(arr, arrElemet{k, v.lines, v.commits, v.files})
	}

	startSort(arr)
	print(arr)
}
