using System.Xml.Linq;
using System.Xml.Serialization;
using _353504_Antonava_Lab5.Domain;
using Newtonsoft.Json;
public class Serializer : ISerializer
{
	public IEnumerable<Runway> DeSerializeByLINQ(string fileName)
	{
		var xDocument = XDocument.Load(fileName);

		var runways = from runway in xDocument.Descendants("Runway")
					  select new Runway
					  {
						  Name = runway.Element("Name")?.Value,
						  Length = (int)runway.Element("Length"),
						  Width = (int)runway.Element("Width")
					  };

		return runways.ToList();
	}

	public IEnumerable<Runway> DeSerializeXML(string fileName)
	{
		var serializer = new XmlSerializer(typeof(List<Runway>));
		using (var stream = new FileStream(fileName, FileMode.Open))
		{
			return (List<Runway>)serializer.Deserialize(stream);
		}
	}

	public IEnumerable<Runway> DeSerializeJSON(string fileName)
	{
		var json = File.ReadAllText(fileName);
		return JsonConvert.DeserializeObject<List<Runway>>(json);
	}

	public void SerializeByLINQ(IEnumerable<Runway> runways, string fileName)
	{
		var xElement = new XElement("Runways",
			from runway in runways
			select new XElement("Runway",
				new XElement("Name", runway.Name),
				new XElement("Length", runway.Length),
				new XElement("Width", runway.Width)
			));

		xElement.Save(fileName);
	}

	public void SerializeXML(IEnumerable<Runway> runways, string fileName)
	{
		var serializer = new XmlSerializer(typeof(List<Runway>));
		using (var stream = new FileStream(fileName, FileMode.Create))
		{
			serializer.Serialize(stream, runways.ToList());
		}
	}

	public void SerializeJSON(IEnumerable<Runway> runways, string fileName)
	{
		var json = JsonConvert.SerializeObject(runways);
		File.WriteAllText(fileName, json);
	}
}