using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static PFTestV5.Program.Item;

namespace PFTestV5
{
    class Program
    {
        static void Main()
        {

            Test2();

        }

        public class Dice
        {
            private static readonly Random Rand = new Random();

            public static int Roll(int sides)
            {
                return Rand.Next(1, (sides + 1));
            }

        }

        public class Battle
        {

        }

        public class Character
        {
            public string CharName { get; private set; }

            public int HitDieCnt { get; private set; }
            public int Level { get; private set; }

            public List<string> CharClassName { get; private set; }

            public string CharRaceName { get; private set; }
            public int Strength { get; private set; }
            public int Dexterity { get; private set; }
            public int Constitution { get; private set; }
            public int Intelligence { get; private set; }
            public int Wisdom { get; private set; }
            public int Charisma { get; private set; }

            public int StrMod { get; private set; }
            public int DexMod { get; private set; }
            public int ConMod { get; private set; }
            public int IntMod { get; private set; }
            public int WisMod { get; private set; }
            public int ChaMod { get; private set; }

            public int SizeMod { get; private set; }

            public int BaseAttackBonus { get; private set; }
            public int ArmorClass { get; private set; }
            public int TouchArmorClass { get; private set; }
            public int FlatFootedArmorClass { get; private set; }
            public int Initiative { get; private set; }

            public int MaxHealth { get; private set; }
            public int CurrentHealth { get; set; }

            public int FortitudeSave { get; private set; }
            public int ReflexSave { get; private set; }
            public int WillSave { get; private set; }

            public int AttackMod { get; private set; }

            public int WeaponHands { get; private set; }
            public int DamageMod { get; private set; }

            public int CombatManeuverBonus { get; private set; }
            public int CombatManeuverDefense { get; private set; }

            public string EquippedWeaponName { get; private set; }

            public int WeaponBonus { get; private set; }
            public int CharDamageDiceCnt { get; private set; }
            public int CharDamageDiceSize { get; private set; }
            public int CharCritRange { get; private set; }
            public int CharCritMod { get; private set; }

            public int Speed { get; private set; }

            // Locations
            public int CoordX { get; private set; }
            public int CoordY { get; private set; }
            public int CoordZ { get; private set; }

            public Character()
            {
                CharName = "Neil";

                CharClassName = new List<string>();

                CoordX = 0;
                CoordY = 0;
                CoordZ = 0;
            }

            public Character(string name)
            {
                CharName = name;

                CharClassName = new List<string>();

                CoordX = 0;
                CoordY = 0;
                CoordZ = 0;
            }


            public static void PrintStats(Character character)
            {

                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(character))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(character);
                    Console.WriteLine("{0} = {1}", name, value);
                    Console.WriteLine();
                }

            }


            // Set this First
            public void SetCharAbilityScores(int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
            {
                Strength = strength;
                Dexterity = dexterity;
                Constitution = constitution;
                Intelligence = intelligence;
                Wisdom = wisdom;
                Charisma = charisma;

                if (Strength >= 10)
                {
                    StrMod = (int)Math.Floor((decimal)((Strength - 10) / 2));
                }
                else if ((Strength < 10) && (Strength > 0))
                {
                    StrMod = (int)Math.Floor((decimal)((Strength - 11) / 2));
                }

                if (Dexterity >= 10)
                {
                    DexMod = (int)Math.Floor((decimal)((Dexterity - 10) / 2));
                }
                else if ((Dexterity < 10) && (Dexterity > 0))
                {
                    DexMod = (int)Math.Floor((decimal)((Dexterity - 11) / 2));
                }

                if (Constitution >= 10)
                {
                    ConMod = (int)Math.Floor((decimal)((Constitution - 10) / 2));
                }
                else if ((Constitution < 10) && (Constitution > 0))
                {
                    ConMod = (int)Math.Floor((decimal)((Constitution - 11) / 2));
                }

                if (Intelligence >= 10)
                {
                    IntMod = (int)Math.Floor((decimal)((Intelligence - 10) / 2));
                }
                else if ((Intelligence < 10) && (Intelligence > 0))
                {
                    IntMod = (int)Math.Floor((decimal)((Intelligence - 11) / 2));
                }

                if (Wisdom >= 10)
                {
                    WisMod = (int)Math.Floor((decimal)((Wisdom - 10) / 2));
                }
                else if ((Wisdom < 10) && (Wisdom > 0))
                {
                    WisMod = (int)Math.Floor((decimal)((Wisdom - 11) / 2));
                }

                if (Charisma >= 10)
                {
                    ChaMod = (int)Math.Floor((decimal)((Charisma - 10) / 2));
                }
                else if ((Charisma < 10) && (Charisma > 0))
                {
                    ChaMod = (int)Math.Floor((decimal)((Charisma - 11) / 2));
                }

                ArmorClass = 10 + DexMod;
                TouchArmorClass = 10 + DexMod;
                FlatFootedArmorClass = ArmorClass - DexMod;

                Initiative = DexMod;
            }


            // Set this Second
            public class Race
            {
                public string RaceName { get; private set; }

                public int RaceStr { get; private set; }
                public int RaceDex { get; private set; }
                public int RaceCon { get; private set; }
                public int RaceInt { get; private set; }
                public int RaceWis { get; private set; }
                public int RaceCha { get; private set; }

                public int RaceSizeMod { get; private set; }

                public string RaceType { get; private set; }
                public string RaceSubType { get; private set; }

                public int RaceSpeed { get; private set; }

                public Race(int raceID)
                {
                    DataClasses1DataContext pfdb = new DataClasses1DataContext();
                    System.Data.Linq.Table<RACE> cRaces = pfdb.RACEs;


                    IEnumerable<RACE> raceQuery =
                        from RACE in cRaces
                        where RACE.RACE_ID == raceID
                        select RACE;

                    foreach (RACE cRace in raceQuery)
                    {
                        RaceName = cRace.RACE_NAME;

                        RaceStr = cRace.RACE_STR;
                        RaceDex = cRace.RACE_DEX;
                        RaceCon = cRace.RACE_CON;
                        RaceInt = cRace.RACE_INT;
                        RaceWis = cRace.RACE_WIS;
                        RaceCha = cRace.RACE_CHA;

                        RaceSizeMod = cRace.RACE_SIZE_MOD;

                        RaceType = cRace.RACE_TYPE;
                        RaceSubType = cRace.RACE_SUBTYPE;

                        RaceSpeed = cRace.RACE_SPEED;
                    }
                }

            }

            public void SetCharRace(int cRaceId)
            {

                Race charRace = new Race(cRaceId);

                CharRaceName = charRace.RaceName;

                int raceStr = Strength + charRace.RaceStr;
                int raceDex = Dexterity + charRace.RaceDex;
                int raceCon = Constitution + charRace.RaceCon;
                int raceInt = Intelligence + charRace.RaceInt;
                int raceWis = Wisdom + charRace.RaceWis;
                int raceCha = Charisma + charRace.RaceCha;

                SetCharAbilityScores(raceStr, raceDex, raceCon, raceInt, raceWis, raceCha);

                SizeMod = charRace.RaceSizeMod;

                Speed = charRace.RaceSpeed;

            }


            //Set this Third
            public class BaseClass
            {
                public string ClassName { get; private set; }

                public int HitDieSize { get; private set; }

                private readonly decimal BABProgression;

                private readonly decimal FortSaveProgression;
                private readonly decimal RefSaveProgression;
                private readonly decimal WillSaveProgression;

                public int BAB { get; private set; }

                public int FORT { get; private set; }
                public int REF { get; private set; }
                public int WILL { get; private set; }

                public int SkillRankMod { get; private set; }


                public BaseClass(int classID, int hitDieCnt)
                {

                    DataClasses1DataContext pfdb = new DataClasses1DataContext();
                    System.Data.Linq.Table<BASE_CLASS> bclasses = pfdb.BASE_CLASSes;


                    IEnumerable<BASE_CLASS> classQuery =
                        from BASE_CLASS in bclasses
                        where BASE_CLASS.CLASS_ID == classID
                        select BASE_CLASS;

                    foreach (BASE_CLASS bclass in classQuery)
                    {
                        ClassName = bclass.CLASS_NAME;


                        HitDieSize = bclass.CLASS_HITDIE;


                        BABProgression = bclass.CLASS_BAB_PROGRESSION;

                        FortSaveProgression = bclass.CLASS_FORT_PROGRESSION;
                        RefSaveProgression = bclass.CLASS_REF_PROGRESSION;
                        WillSaveProgression = bclass.CLASS_WILL_PROGRESSION;

                        BAB = (int)Math.Floor(BABProgression * hitDieCnt);

                        FORT = (int)Math.Floor(hitDieCnt / FortSaveProgression);
                        REF = (int)Math.Floor(hitDieCnt / RefSaveProgression);
                        WILL = (int)Math.Floor(hitDieCnt / WillSaveProgression);

                        if (FortSaveProgression == 2)
                        {
                            FORT += 2;
                        }

                        if (RefSaveProgression == 2)
                        {
                            REF += 2;
                        }

                        if (WillSaveProgression == 2)
                        {
                            WILL += 2;
                        }


                        SkillRankMod = bclass.CLASS_SKILL_RANK_MOD;
                    }
                }
            }

            public void AddClassLevel(int cClassId, int cClassLevel)
            {

                HitDieCnt += cClassLevel;

                Level += HitDieCnt;


                BaseClass charClass = new BaseClass(cClassId, cClassLevel);



                CharClassName.Add(charClass.ClassName);


                if (MaxHealth == 0)
                {
                    MaxHealth += charClass.HitDieSize + ConMod;
                }

                for (int i = 2; i < cClassLevel; i++)
                {
                    MaxHealth += Dice.Roll(charClass.HitDieSize) + ConMod;
                }

                CurrentHealth += MaxHealth;

                BaseAttackBonus += charClass.BAB;

                FortitudeSave += ConMod + charClass.FORT;
                ReflexSave += DexMod + charClass.REF;
                WillSave += WisMod + charClass.WILL;

                UpdateOffenseStats();

            }


            private void UpdateOffenseStats()
            {
                CombatManeuverBonus = BaseAttackBonus + StrMod + (-1 * SizeMod);

                CombatManeuverDefense = CombatManeuverBonus + 10 + DexMod;

                if (WeaponHands == 0)
                {
                    WeaponHands = 1;
                }

                AttackMod = BaseAttackBonus + StrMod + (-1 * SizeMod) + WeaponBonus;

                DamageMod = BaseAttackBonus + (int)(StrMod * (.5 + (.5 * WeaponHands))) + WeaponBonus;

            }


            public void EquipWeapon(int charWeaponId, int hands, int weaponBonus)
            {
                Weapon Greatsword = new Weapon(charWeaponId);

                EquippedWeaponName = Greatsword.WeaponName;

                WeaponBonus = weaponBonus;

                WeaponHands = hands;

                CharCritRange = Greatsword.CritRange;

                CharCritMod = Greatsword.CritMod;

                CharDamageDiceCnt = Greatsword.DamageDieCnt;

                CharDamageDiceSize = Greatsword.DamageDieSize;

                UpdateOffenseStats();

            }


            public void MoveTo(int x, int y, int z)
            {
                CoordX = x;
                CoordY = y;
                CoordZ = z;
            }



            public void AttackAction(Character victim)
            {
                int attackRoll = Dice.Roll(20);
                int attack = attackRoll + AttackMod;

                int confirmationRoll = Dice.Roll(20);
                int confirmation = confirmationRoll + AttackMod;

                int damageRoll = DamageRoll();
                if (attackRoll == 1)
                {
                    Console.WriteLine("Critical Miss!");
                }
                else if ((attack >= victim.ArmorClass) || (attackRoll == 20))
                {
                    Console.WriteLine();
                    Console.WriteLine("Sucessfull Hit!");
                    Console.WriteLine();

                    if (attackRoll >= CharCritRange)
                    {
                        Console.WriteLine("Critical Threat...");
                        Console.WriteLine();

                        if ((confirmationRoll == 20) || (confirmation >= victim.ArmorClass))
                        {
                            Console.WriteLine("CRITICAL HIT!");

                            for (int i = 0; i < (CharCritMod - 1); i++)
                            {
                                damageRoll += DamageRoll();
                            }

                        }
                        else
                        {
                            Console.WriteLine("Failed to confirm.");
                            Console.WriteLine();
                        }
                    }
                    Console.WriteLine(CharName + " dealt " + damageRoll + " points of damage to " + victim.CharName + "!");
                    Console.WriteLine();

                    victim.CurrentHealth -= damageRoll;

                }
                else
                {
                    Console.WriteLine(CharName + " missed " + victim.CharName + " with a " + attack);
                }

            }

            private int DamageRoll()
            {
                int damage = DamageMod + DamageDiceRoll();
                
                return damage;
            }

            private int DamageDiceRoll()
            {
                int rollTotal = 0;

                for (int i = 0; i < (CharDamageDiceCnt - 1); i++)
                {
                    rollTotal += Dice.Roll(CharDamageDiceSize);
                }

                return rollTotal;
            }

            //Sets stats to that of a Porter's and equips them with a +5 Greatsword;
            public void CreatePorter()
            {
                SetCharAbilityScores(15, 9, 12, 10, 8, 11);

                SetCharRace(1);

                AddClassLevel(1, 1);

                EquipWeapon(1, 2, 5);

                MoveTo(5, 0, 0);
            }

        }

        public class Item
        {
            public string ItemName { get; private set; }
            public int ItemPrice { get; private set; }

            public Item()
            {

            }

            public class Weapon
            {

                public string WeaponName
                {
                    get; private set;
                }
                public int DamageDieCnt { get; private set; }
                public int DamageDieSize { get; private set; }

                public int CritRange { get; private set; }
                public int CritMod { get; private set; }


                public Weapon(int weaponId)
                {
                    DataClasses1DataContext pfdb = new DataClasses1DataContext();
                    System.Data.Linq.Table<WEAPON> weapons = pfdb.WEAPONs;

                    IEnumerable<WEAPON> weaponQuery =
                        from WEAPON in weapons
                        where WEAPON.WEAPON_ID == weaponId
                        select WEAPON;

                    foreach (WEAPON wEAPON in weaponQuery)
                    {
                        WeaponName = wEAPON.WEAPON_NAME;
                        DamageDieCnt = wEAPON.WEAPON_DAMAGE_DIE_COUNT;
                        DamageDieSize = wEAPON.WEAPON_DAMAGE_DIE_SIZE;
                        CritRange = wEAPON.WEAPON_CRIT_RANGE;
                        CritMod = wEAPON.WEAPON_CRIT_MODIFIER;
                    }
                }

            }

        }


        public static void Test1()
        {
            Character jeff = new Character("jeff");

            Character.PrintStats(jeff);

            jeff.SetCharAbilityScores(31, 10, 23, 16, 17, 16);

            jeff.SetCharRace(1);

            jeff.AddClassLevel(3, 5);

            jeff.EquipWeapon(1, 2, 0);

            jeff.MoveTo(30, 0, 0);

            Character.PrintStats(jeff);

            Console.WriteLine();

            Console.ReadLine();
        }

        public static void Test2()
        {

            Character npc = new Character("Porter");


            npc.CreatePorter();

            Character jeff = new Character("Jeff");

                jeff.SetCharAbilityScores(31, 10, 23, 16, 17, 16);
                
                jeff.SetCharRace(3);
                
                jeff.AddClassLevel(3, 5);
                
                jeff.EquipWeapon(1, 2, 0);
                
                jeff.MoveTo(0, 0, 0);


            while (npc.CurrentHealth >= 0)
            {
                Console.WriteLine(npc.CurrentHealth);

                jeff.AttackAction(npc);
            }

            Console.WriteLine(npc.CurrentHealth);


            Console.ReadLine();
        }


    }


}
