using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;

public interface Syarat_Pemain
{
    void serang(Karakter target_serangan);
    void gunakanSkill(string skill, Karakter target_serangan);
    void cetakInformasi();
}
public abstract class Attribute_Method_Karakter : Syarat_Pemain
{
    public string nama_karakter;
    public int HP;
    public int MP;
    public int Kekuatan;

    public Attribute_Method_Karakter(string nama, int hp, int mp, int kekuatan)
    {
        nama_karakter = nama;
        HP = hp;
        MP = mp;
        Kekuatan = kekuatan;
    }

    public abstract void serang(Karakter target);
    public abstract void gunakanSkill(string skill, Karakter target_serangan);
    public abstract void cetakInformasi();
}

public class Karakter : Attribute_Method_Karakter
{
    public int pengurangan_MN_basic;
    public int pengurangan_MN_skill;

    public Karakter(string nama, int hp, int mp, int kekuatan, int pengurangan_MN_basic, int pengurangan_MN_skill) : base(nama, hp, mp, kekuatan)
    {
        this.pengurangan_MN_basic = pengurangan_MN_basic;
        this.pengurangan_MN_skill = pengurangan_MN_skill;
    }

    public override void serang(Karakter target)
    {
        if (MP > pengurangan_MN_basic)
        {
            MP = MP - pengurangan_MN_basic;
            target.HP -= Kekuatan;
            Console.WriteLine($"Melakukan serangan Basic Attack sebesar {Kekuatan} terhadap {target.nama_karakter}");
        }
        else
        {
            Console.WriteLine("Mana Power tidak cukup!\nLakukan Healing!");
        }
    }


    public override void gunakanSkill(string skill, Karakter target_serangan)
    {
        if (skill == "Fireball")
        {
            if (MP > pengurangan_MN_skill)
            {
                MP -= pengurangan_MN_skill * 2;
                target_serangan.HP -= Kekuatan * 3;
                Console.WriteLine($"{target_serangan.nama_karakter} diserang dengan Fireball\nHP Musuh berkurang sebesar {Kekuatan * 2}\nHP musuh tersisa {target_serangan.HP}");
            }
            else
            {
                Console.WriteLine("Mana Power tidak cukup!\nLakukan Healing!");
                Thread.Sleep(2000);
            }

        }
        else if (skill == "Ice Blast") // Ice Blast 
        {
            if (MP > pengurangan_MN_skill)
            {
                MP -= pengurangan_MN_skill * 2;
                target_serangan.HP -= Kekuatan * 3;
                Console.WriteLine($"{target_serangan.nama_karakter} diserang dengan Ice Blast\nHP Musuh berkurang sebesar {Kekuatan * 3} dan melambat\nHP musuh tersisa {target_serangan.HP}");
            }
            else
            {
                Console.WriteLine("Mana Power tidak cukup!\nLakukan Healing!");
                Thread.Sleep(2000);
            }
        }
        else // Healing
        {
            HP += 200; // jumlah HP Healing
            MP += 30; // Jumlah Mana Healing
            Console.WriteLine("Healing... HP + 200 , Mana + 30");
        }
    }

    public override void cetakInformasi()
    {
        Console.WriteLine("--------------------------------------------------------------------------");
        Console.WriteLine("Informasi Karaktermu");
        Console.WriteLine($"karakter           : {nama_karakter}");
        Console.WriteLine($"HP Tersisa         : {HP}");
        Console.WriteLine($"Mana Power tersisa : {MP}");
        Console.WriteLine("--------------------------------------------------------------------------");
        Console.WriteLine("Serangan dan Kemampuan yang dapat kamu gunakan:");
        Console.WriteLine($"1.Basic Attack : {Kekuatan}");
        Console.WriteLine($"2.Fireball     : {Kekuatan * 2}");
        Console.WriteLine($"3.Ice Blast    : {Kekuatan * 3} dan melambat");
        Console.WriteLine($"4.Heal");
        Console.WriteLine("--------------------------------------------------------------------------");
    }
}
public class Program
{
    static void Main(string[] args)
    {
        Karakter Pemain = new Karakter("Edwin", 6700, 200, 300, 10, 30);
        Karakter Musuh = new Karakter("Eka", 6500, 180, 230, 9, 25);

        Console.WriteLine($"Ayo adu si {Pemain.nama_karakter} dan {Musuh.nama_karakter}\nEnter untuk memulai..... ");
        Console.ReadLine();
        while (Musuh.HP > 0)
        {
            Console.WriteLine("===========================================================================");
            Pemain.cetakInformasi();
            Console.WriteLine($"HP si {Musuh.nama_karakter} : {Musuh.HP}");
            Console.WriteLine($"Ayo {Pemain.nama_karakter} serangg!!!");
            Console.WriteLine("===========================================================================");
            string pilihan = Console.ReadLine();

            if (pilihan == "1") // Basic Attact/ serangan biasa
            {
                Console.WriteLine("Menyerang lawan dengan basic attack");
                Thread.Sleep(1500);
                Pemain.serang(Musuh);
                Console.WriteLine($"Lawan diserang !! Hp Lawan Saat ini {Musuh.HP}");
                Thread.Sleep(1500);
            }
            else if (pilihan == "2")// FIreball
            {
                Thread.Sleep(1500);
                Console.WriteLine("Menyerang lawan dengan Fireball!!");
                Pemain.gunakanSkill("Fireball", Musuh);
                Thread.Sleep(1500);
            }
            else if (pilihan == "3")// Ice Blast
            {
                Thread.Sleep(1500);
                Console.WriteLine("Menyerang lawan dengan Ice Blast!!");
                Pemain.gunakanSkill("Ice Blast", Musuh);
                Thread.Sleep(1500);
            }
            else // Healing 
            {
                Thread.Sleep(1500);
                Console.WriteLine("Healing................");
                Pemain.gunakanSkill("Healing", Musuh);
                Thread.Sleep(1500);
            }
        }
        Console.WriteLine("Horee Kamu Menang!!!!");
        Thread.Sleep(2500);
    }
}
// AKhirnya selesai