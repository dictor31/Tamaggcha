using System;

Tamagocha tamagocha = new Tamagocha { Name = "Варфоломей" };
tamagocha.HungryChanged += Tamagocha_HungryChanged;

ConsoleKeyInfo command;
do
{
    command = Console.ReadKey();
    if (command.Key == ConsoleKey.F)
        tamagocha.Feed();
    else if (command.Key == ConsoleKey.I)
        tamagocha.PrintInfo();
}
while (command.Key != ConsoleKey.Escape);
tamagocha.Stop();

void Tamagocha_HungryChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 0);
    Console.Write($"{tamagocha.Name} голодает! Показатель голода растет: {tamagocha.Hungry}");
    Console.SetCursorPosition(0, 5); // возвращаем курсор для ввода команды!
}

class Tamagocha
{ 
    public string Name { get; set; }
    public int Health { get; set; } = 100;
    public int Hungry
    {
        get => hungry;
        set { 
            hungry = value;
            HungryChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public int Dirty { get; set; } = 0;
    public int Thirsty { get; set; } = 0;
    public bool IsDead { get; set; } = false;

    public event EventHandler HungryChanged;

    public Tamagocha()
    {
        Thread thread = new Thread(LifeCircle);
        thread.Start();
    }
    Random random = new Random();
    private int hungry = 0;

    private void LifeCircle(object? obj)
    {
        while (!IsDead)
        {
            Thread.Sleep(500);
            int rnd = random.Next(0, 2);
            switch(rnd)
            {
                case 0: JumpMinute(); break;
                case 1: FallSleep(); break;
                case 2: break;
                case 3: break;
                case 4: break;
                case 5: break;
                default: break;
            }
        }
    }

    private void FallSleep()
    {
        WriteMessageToConsole($"{Name} внезапно начинает спать как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!");
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
    }

    private void JumpMinute()
    {
        WriteMessageToConsole($"{Name} внезапно начинает прыгать как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!");
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
    }

    private void WriteMessageToConsole(string message)
    {
        Console.SetCursorPosition(0, 10);
        Console.Write(message);
        Console.SetCursorPosition(0, 5); // возвращаем курсор для ввода команды!
    }

    public void PrintInfo()
    {
        Console.SetCursorPosition(0, 8);
        Console.WriteLine($"{Name}: Health:{Health} Hungry:{Hungry} Dirty:{Dirty} Thirsty:{Thirsty} IsDead:{IsDead}");
    }

    public void Stop()
    {
        IsDead = true;
    }

    internal void Feed()
    {
        WriteMessageToConsole($"{Name} внезапно начинает ЖРАТЬ как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!");

        Hungry -= random.Next(5, 10);
    }
}