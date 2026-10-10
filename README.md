# Enigma simulátor v C# OOP

Objektový simulátor šifrovacího stroje Enigma I (Wehrmacht) v C# . Cílem projektu je pochopit, jak Enigma funguje, a převést ji do objektového paradigmatu.

## Jak Enigma fachčí?

Při stisku klávesy se stane toto:

1. **Rotory se otočí** (krokování).
2. **Signál** projde cestou: plugboard → pravý rotor → prostřední rotor → levý rotor → reflektor → levý → prostřední → pravý → plugboard.
3. Svítí lampička se **zašifrovaným písmenem**.

Každá součástka jen přehazuje písmena. Síla šifry je v tom, že se rotory s každým písmenem otáčejí, takže se mapování pořád mění.

- **Šifrování a dešifrování je totéž.** Se stejným nastavením stroj zašifrovaný text dešifruje tj symetrické šifrování).
- **Písmeno se nikdy nezašifruje samo na sebe(to je slabina a lowkey chyba..pokud by to tak dělala tak je to vic safe(zdroj docent Klřík:)))).**

## Architektura

Písmena jsou v kódu čísla 0-25 (A=0 ... Z=25). Každá součástka je třída s jednou zodpovědností.

| Třída | Role |
|---|---|
| `IComponent` | rozhraní: každá součástka umí `Forward(int c)` |
| `Plugboard` | prohazuje dvojice písmen (např. `"AB CD"`) |
| `Reflector` | pevný symetrický překlad, který signál otočí zpět |
| `Rotor` | pevné zapojení a zarážka, mění se jen pozice; `Forward`, `Backward`, `Step`, `AtNotch` |
| `EnigmaMachine` | skládá součástky, řídí krokování a pořadí volání |

`Plugboard`, `Reflector` a `Rotor` implementují `IComponent` (polymorfismus). Stroj nezná vnitřek slotů, jen ví, že umí přeložit písmeno.**zdroj PP3 OOP docent Krupka**

### Rotor

Rotor má pevné zapojení (tabulka `_forward` a její inverze `_backward` pro cestu zpět), zarážku a pozici. Průchod rotorem:

```
vstup  = (c + Position) mod 26
výstup = (tabulka[vstup] - Position) mod 26
```

### Krokování

Zarážky se čtou před posunem a krokování proběhne před průchodem signálu:

1. Right rotor se posune vždy.
2. If je right rotor na zarážce, posune se i mid.
3. If je mid rotor na zarážce, posune se sám a vezme s sebou left (dabl stepping).

## Použité rotory

| Rotor | Zapojení | Zarážka |
|---|---|---|
| I | `EKMFLGDQVZNTOWYHXUSPAIBRCJ` | Q |
| II | `AJDKSIRUXBLHWTMCQGZNPYFVOE` | E |
| III | `BDFHJLCPRTXVZNYEIWGAKMUSQO` | V |

Reflektor B: `YRUHQSLDPXNGOKMIEBFZCWVJAT`

## Spuštění

```
dotnet run
```

## Testovací vektor

Rotory zleva doprava III-II-I (pravý rotor je III), reflektor B, pozice AAA, bez plugboardu. Pětkrát stisknuté `A` dá `BDZGO`.

```csharp
var machine = new EnigmaMachine(
    new Plugboard(""),
    new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT"),
    new Rotor("BDFHJLCPRTXVZNYEIWGAKMUSQO", 0, 'V'),   // right: III
    new Rotor("AJDKSIRUXBLHWTMCQGZNPYFVOE", 0, 'E'),   // mid: II
    new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", 0, 'Q'));  // left: I

string result = "";
for (int i = 0; i < 5; i++)
    result += (char)('A' + machine.EncryptChar(0));

Console.WriteLine(result);   // BDZGO
```

Pořadí rotorů je součást klíče: při opačném pořadí vyjde jiný výstup.
