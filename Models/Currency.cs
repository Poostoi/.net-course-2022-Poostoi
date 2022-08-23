namespace Models;

public struct Currency
{
    public Currency(int code, string name)
    {
        Code = code;
        Name = name;
    }

    public int Code { get; set; }
    public string Name { get; set; }
}