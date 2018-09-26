using UnityEngine;
using System.Collections;
using System.IO;

public class IORead {

	public static bool IsExists(string address) {
	
		return File.Exists(address);
	
	}

	public static string GameInfo(string Address) {
	
//		using (FileStream fs = new FileStream(Address, FileMode.OpenOrCreate, FileAccess.Read)) {
//		
//
//		
//		}

		//FileStream fs = new FileStream(Address, FileMode.Open, FileAccess.Read);
		string info = null;

		using (StreamReader streamReader = new StreamReader(Address)) {

			info = streamReader.ReadToEnd().ToString();
		
		}

		return info;

	}

	public static void SaveToFile(string Address, string data) {
	
		Debug.Log("IORead-------:" + Address);
//		FileStream fsFile = new FileStream(Address,FileMode.OpenOrCreate);
//		StreamWriter swWriter = new StreamWriter(fsFile);
//		swWriter.WriteLine(data);
//		swWriter.Close();

		//using (FileStream fs = new FileStream(Address, FileMode.OpenOrCreate, FileAccess.Read)) {
		
		using (StreamWriter swWriter = new StreamWriter(Address)) {

			swWriter.WriteLine(data);
		
		}
		
		//}
	
	}

	public static void SaveBinaryFile(string Address, byte[] byteData) {
	
		//Address = Data.FILE_ADRESS + Address;

		Debug.Log("SaveBinaryFile:" + Address);

		using (FileStream fs = new FileStream(Address, FileMode.Create)) {
		
			using (BinaryWriter bw = new BinaryWriter(fs)) {

				bw.Write(byteData);
			
			}
		
		}
	
	}

	public static byte[] ReadyBinaryFile(string Address) {
	
		//Address = Data.FILE_ADRESS + Address;

		Debug.Log("ReadyBinaryFile:" + Address);

		byte[] buffer = null;

		using (FileStream fs = new FileStream(Address, FileMode.Open)) {
			
			using (BinaryReader br = new BinaryReader(fs)) {

				buffer = br.ReadBytes((int)fs.Length);
			
			}
			
		}

		return buffer;
	
	}
	
}
