using System;

public class Class1
{
	public Class1()
	{
		Request request = new Request();

		request.RecursionDesired = true;
		request.Id = 123;

		UdpClient udp = new UdpClient();
		IPEndPoint google = new IPEndPoint(IPAddress.Parse("8.8.8.8"), 53);

		// Send to google's DNS server
		await udp.SendAsync(request.ToArray(), request.Size, google);

		UdpReceiveResult result = await udp.ReceiveAsync();
		byte[] buffer = result.Buffer;
		Response response = Response.FromArray(buffer);

		// Outputs a human readable representation
		Console.WriteLine(response);

	}
}
