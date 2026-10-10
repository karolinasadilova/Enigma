using System.Diagnostics;

namespace Enigma;
/*/
 rotor=kotouc, 26 kontaktu(26pismen)
 
 _forward..draty v kotoucu tam, posune vstup o otoceni
 _backward..draty smer zpet
 Position..o kolik je  kotouc otoceny
 _notch.. kotouc postrci sousedni rotor
 _ring..nastaveni krouzku vs kotouc
 
 jedine co se meni je pos!
 2 tbl= 2dráty
 
 
 
    /*/
public class Rotor: IComponent
{
    private readonly int[] _forward= new int[26];//tam zapojeni
    private readonly int[] _backward= new int[26];//zpatky zapojeni
    private readonly int _notch;
    //private readonly int _ring;
    public int Position { get; private set; }
    public bool Atnotch => Position == _notch;
    public Rotor(string wiring, int position, char notch)
    {
        for (int i = 0; i < 26; i++)
        {
             _forward[i]=wiring[i] - 'A';
             _backward[_forward[i]]=i;//inverze
        }
        _notch = notch-'A';
        Position = position;
    }

    public static int Mod(int el)
    {
        return ((el % 26)+ 26) % 26;
    }

    public int Forward(int c)
    {
        int entry = Mod(c + Position);
        int exit=_forward[entry];
        return Mod(exit - Position);
        
    }

    public int Backward(int c)
    {
        int entry = Mod(c + Position);
        int exit = _backward[entry];
        return Mod(exit - Position);
    }

    public void step()
    {
        Position = Mod(Position + 1);
    }
}