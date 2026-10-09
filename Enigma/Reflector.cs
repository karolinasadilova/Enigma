namespace Enigma;

public class Reflector : IComponent
{
    private readonly int[] _nmap=new int[26];

    public Reflector(string wiring)
    {
        for (int i = 0; i < 26; i++)
        {
            _nmap[i] = wiring[i] - 'A';
            
        }
    }

    public int Forward(int c) => _nmap[c];
    
}