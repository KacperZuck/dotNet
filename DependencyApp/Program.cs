namespace DependencyApp
{

    public interface ITool
    {
        void SetHammer(Hammer Ihammer);
        void SetSaw(Saw Isaw);
    }
    public class Hammer
    {
        public void Use()
        {
            Console.WriteLine("Hammering...");
        }
    }

    public class Saw
    {
        public void Use()
        {
            Console.WriteLine("Wood...");
        }
    }

    public class Builder : ITool
    {
        // Injection by interface
        private Hammer hammer;
        private Saw saw;
        public void BuilderHouse()
        {
            hammer.Use();
            saw.Use();
            Console.WriteLine("Builded");

        }

        public void SetHammer(Hammer Ihammer)
        {
            hammer = Ihammer;
        }

        public void SetSaw(Saw Isaw)
        {
            saw = Isaw;
        }

        // Properties -- for setter Dep In
        //public Hammer Hammer { get; set; }
        //public Saw Saw { get; set; }


        //private Hammer _hammer;
        //private Saw _saw;

        // DEPENDENCY
        //private Builder()
        //{
        //    // builder have DEPENDENCY ON :
        //    _hammer = new Hammer();
        //    _saw = new Saw();
        //}

        // Constructor Dependency INJECTION
        //public Builder(Hammer hammer, Saw saw)
        //{
        //    _hammer = hammer;
        //    _saw = saw;
        //}

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Hammer hammer = new Hammer();
            Saw saw = new Saw();
            Builder builder = new Builder();
        
            builder.SetHammer(hammer);    // injection by setters
            builder.SetSaw(saw);
            builder.BuilderHouse();

            Console.ReadLine();
        }
    }
}