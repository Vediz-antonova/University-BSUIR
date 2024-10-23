using _353504_Antonava_Lab5.Domain;
public interface ISerializer
{
	IEnumerable<Runway> DeSerializeByLINQ(string fileName);
	IEnumerable<Runway> DeSerializeXML(string fileName);
	IEnumerable<Runway> DeSerializeJSON(string fileName);
	void SerializeByLINQ(IEnumerable<Runway> runways, string fileName);
	void SerializeXML(IEnumerable<Runway> runways, string fileName);
	void SerializeJSON(IEnumerable<Runway> runways, string fileName);
}