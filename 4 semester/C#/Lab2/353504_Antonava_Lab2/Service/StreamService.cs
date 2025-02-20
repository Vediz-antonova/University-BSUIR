using System.Text;
using System.Text.Json;
using System.Threading;

namespace Service
{
	public class StreamService<T>
	{
		private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
		public async Task WriteToStreamAsync(Stream stream, IEnumerable<T> data, IProgress<string> progress)
		{
			await _semaphore.WaitAsync();
			try
			{
				progress?.Report($"Поток {Thread.CurrentThread.ManagedThreadId}: Начало записи в поток");

				var jsonData = JsonSerializer.Serialize(data);
				var buffer = Encoding.UTF8.GetBytes(jsonData);

				for (int i = 0; i < buffer.Length; i++)
				{
					await stream.WriteAsync(buffer, i, 1);
					await Task.Delay(300);
					progress?.Report($"Записано {i + 1} из {buffer.Length} байт");
				}

				await stream.FlushAsync();
				progress?.Report($"Поток {Thread.CurrentThread.ManagedThreadId}: Запись завершена");
			}
			finally
			{
				_semaphore.Release();
			}
		}

		public async Task CopyFromStreamAsync(Stream stream, string fileName, IProgress<string> progress)
		{
			await _semaphore.WaitAsync();
			try
			{
				progress?.Report($"Поток {Thread.CurrentThread.ManagedThreadId}: Начало копирования из потока");

				stream.Position = 0;

				using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
				{
					await stream.CopyToAsync(fileStream);
					progress?.Report($"Поток {Thread.CurrentThread.ManagedThreadId}: Копирование завершено");
				}
			}
			finally
			{
				_semaphore.Release();
			}
		}
		public async Task<int> GetStatisticsAsync(string fileName, Func<T, bool> filter)
		{
			using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				var data = await JsonSerializer.DeserializeAsync<List<T>>(fileStream);
				return data.Count(filter);
			}
		}
	}
}
