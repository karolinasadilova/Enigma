namespace Enigma;

//prii kazdem pismenu toci rotory
//signal...plugb->rig->mid->left->ref->left->mid->rig->plugb



/*/
 rig se posune vzdy
 if je rig v notchu tak se posune i mid
 if mid na notchu, posune se sam a vezme i left-DABLSTEPPING(historicka sranda)
 
 
 problem:musim zjistit notche(tj zarazky) pred tim nez se posunu
 takze zapisovat stavy do vars
 
    /*/


public class EnigmaMachine
{
    private readonly Plugboard _plugboard;
    private readonly Reflector  _reflector;
    private readonly Rotor _right;
    private readonly Rotor _middle;
    private readonly Rotor _left;


    public EnigmaMachine(Plugboard plugboard, Reflector reflector, Rotor right, Rotor middle, Rotor left)
    {
        _plugboard = plugboard;
        _reflector = reflector;
        _left = left;
        _middle = middle;
        _right = right;
        
    }
    
    
    private void StepRotors()
    {   
    bool middleAtNotch = _middle.Atnotch;
    bool rightAtNotch = _right.Atnotch;

    if (middleAtNotch)
    {
        _left.step();
        _middle.step();
    }
    else if (rightAtNotch)
    {
        _middle.step();
    }
    _right.step();
    }

    public int EncryptChar(int key)
    {
        //c je signal, po kazdm slotu se zmeni
        StepRotors();
        int c = _plugboard.Forward(key);
        c = _right.Forward(c);
        c = _middle.Forward(c);
        c = _left.Forward(c);
        
        c =_reflector.Forward(c);
        
        c = _left.Backward(c);
        c = _middle.Backward(c);
        c = _right.Backward(c);

        c = _plugboard.Forward(c);
        return c;

    }


    
}