namespace Enigma;

public class Plugboard : IComponent//prohazovac pismen
{
 private readonly int[] _nmap = new int[26]; //soukrome pole


//_nmap=tbl kam se kazde pismeno prelozi
 public Plugboard(string pairs)//konstruktor~zapojeni kabelu
 {
  for (int i = 0; i < 26; i++)
  {
   _nmap[i] = i;
  }

  foreach (var pair in pairs.Split(' ', StringSplitOptions.RemoveEmptyEntries))
  {
   int a = pair[0] - 'A';
   int b = pair[1] - 'A';
   
   _nmap[a] = b;
   _nmap[b] = a;
   
  }
 }

 public int Forward(int c) => _nmap[c];//dostane pismneo, podiva se do tbl a vrati to

}