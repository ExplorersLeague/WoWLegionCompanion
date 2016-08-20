using System;

public class Dustin
{
	public Dustin()
	{
		this.Name = "Dustin Horne";
		this.Age = 34;
		this.Url = "http://www.dustinhorne.com";
	}

	public string Name { get; private set; }

	public int Age { get; private set; }

	public string Url { get; private set; }
}
