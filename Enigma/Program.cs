namespace Enigma;

class Program
{
    /*/
     jak ta vec fachčí:
        1. rotory/rotor umi prelozit pismeno a umi se otocit
        2. proud...klavesa-> plugboard->3rotory->refletor->zpatky symetricky
        3. svití result
        
    mapovani se meni s ∀ pismenem
    
     refletor/umi odrazit pismeno
            =senduje signal zpatky->sifruje symetricky
            =neexistuje pismeno zasifrovane samo na sebe
                
    
    objcts &&classy
    typ rotoru - nema stav - data
    rotor x - ma stav - classa
    ref - nema stav - classa
    plugboard - nema stav - classa
    enigma - ma stav - classa
    
    
    bacha rotor muze byt typ a instance!
    
    co dela co:
    1. rotor: prelozi pismeno, sam se otoci
    2. refletor: odrazi pismeno
    3. plugboard: prohodi pismeno
    4. enigma: neumi sifru, sklada soucastky&&ridi poradi
    
    
    
    OOP princip(pointer na docenta křupku):
            masina nezna sloty, co ktery slot dela, jen vi co jaky slot umi,
            kdyz vymenim rotory-masina furt stejna
            
            
            
    Interface
    ∀ soucastka umi prelozit pismeno: rotor, reflektor, plugboard
    
    Polymorfismus(docent křupka back again):
    ruzne tridy, ktere se navenek chovaji stejne
    
    
    
    Rotor:
    - nejslozitejsi slot
    faze:
        1. zapojeni a zarazka se nemeni-pevne
        2. krouzek-nastaveni
        3. pozice-meni se pri ∀ pismenu
    rotor se otoci && zapojeni vevnitr zustava
    meni se: NA JAKEM MISTE SIGNAL DO ROT VSTOUPI
    "vstup o pozici projde pevnou tbl a vystup posune back"
    
    - collab rotorů
    
    
    - masina je poradi callu 
     */
    
    static void Main(string[] args)
    {
        
        //testovani
        var plug = new Plugboard("AB CD");
        Console.WriteLine(plug.Forward(0));
        Console.WriteLine(plug.Forward(1));
        Console.WriteLine(plug.Forward(4));
        
        
        var plug2 = new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT");
        Console.WriteLine(plug2.Forward(0));
        Console.WriteLine(plug2.Forward(24));


        var rotor = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ",0,'Q');
        Console.WriteLine(rotor.Forward(0));
        Console.WriteLine(rotor.Forward(4));
        var rotor2 = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ",1,'Q');
        Console.WriteLine(rotor2.Forward(0));

        var machine = new EnigmaMachine(
            new Plugboard(""),
            new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT"),
            new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", 0, 'Q'),//left rotor
            new Rotor("AJDKSIRUXBLHWTMCQGZNPYFVOE", 0, 'E'),//mid rotor
            new Rotor("BDFHJLCPRTXVZNYEIWGAKMUSQO", 0, 'V'));//rig rotor

        string res = "";//tady skladam vysledek
        for (int i = 0; i < 5; i++)
        {
            res += (char)('A' + machine.EncryptChar(0));

        }
        Console.WriteLine(res);
    }
}