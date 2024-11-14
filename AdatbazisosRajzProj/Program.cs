using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AdatbazisosRajzProj
{
    //rajz.Pontok.Add(new Pont { X = 1, Y = 1, Color = ConsoleColor.Red, Symbol = 'O', Rajz = rajz });

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Rajz> Draws { get; set; }
        public DbSet<Pont> Points { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RajzAdatbazis;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
    public class Rajz
    {
        [Key]
        public int ID { get; set; }
        [Required, StringLength(100)]
        public string? Name { get; set; }
        public List<Pont> Pontok { get; } = new List<Pont>();
    }
    public class Pont
    {
        [Key]
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        [Required]
        public ConsoleColor? Color { get; set; }
        public char Symbol { get; set; }
        [Required]
        public Rajz Rajz { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] Files = []; 
            ApplicationDbContext context = new ApplicationDbContext();
            context.Draws.Add(new Rajz { Name = "Kör" });
            context.Points.Append(new Pont { X = 1, Y = 1, Color = ConsoleColor.Red, Symbol = 'O', Rajz = { } });
            context.Points.Append(new Pont { X = 2, Y = 2, Color = ConsoleColor.Red, Symbol = 'O', Rajz = { } });
            foreach (var rajz in context.Draws)
            {
                Files.Append(rajz.Name);
                Console.WriteLine(rajz.Name);
                foreach (var pont in rajz.Pontok)
                {
                    Console.WriteLine(pont.X);
                    Console.WriteLine(pont.Y);
                    Console.WriteLine(pont.Color);
                    Console.WriteLine(pont.Symbol);
                    Console.WriteLine(pont.Rajz);
                }
            }
            
            context.SaveChanges();
        }
    }
}