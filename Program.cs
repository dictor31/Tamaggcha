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
	else if (command.Key == ConsoleKey.S)
		tamagocha.Clean();
	else if (command.Key == ConsoleKey.W)
		tamagocha.Drink();
	else if (command.Key == ConsoleKey.G)
		tamagocha.GetGift();
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
		set
		{
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
			Thread.Sleep(600);
			Console.Clear();
			int rnd = random.Next(0, 10);
			switch (rnd)
			{
				case 0: JumpMinute(); break;
				case 1: FallSleep(); break;
				case 2: Sing(); break;
				case 3: FightWin(); break;
				case 4: FightLose(); break;
				case 5: TryDead(); break;
				case 6: Dance(); break;
				case 7: Hide(); break;
				case 8: FindEat(); break;
				case 9: Trance(); break;
				case 10: Yell(); break;
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
	private void FindEat()
	{
		WriteMessageToConsole($"{Name} находит еду. Показатели голода понижены!");
		Thirsty += random.Next(5, 10);
		Hungry -= random.Next(5, 10);
		Dirty += random.Next(5, 10);
	}
	private void Dance()
	{
		WriteMessageToConsole($"{Name} внезапно начинает крутиться на голове! Это продолжается целую минуту. Показатели голода, жажды и чистоты ВДВОЕ повышены!");
		Thirsty += random.Next(5, 20);
		Hungry += random.Next(5, 20);
		Dirty += random.Next(5, 20);
	}
	private void Hide()
	{
		WriteMessageToConsole($"{Name} прячется целый час. Показатели голода, жажды и чистоты повышены!");
		Thirsty += random.Next(5, 10);
		Hungry += random.Next(5, 10);
		Dirty += random.Next(5, 10);
	}
	private void TryDead()
	{
		WriteMessageToConsole($"{Name} притворяется мёртвым. Показатели голода, жажды и чистоты повышены!");
		Thirsty += random.Next(5, 10);
		Hungry += random.Next(5, 10);
		Dirty += random.Next(5, 10);
	}
	private void FightLose()
	{
		WriteMessageToConsole($"{Name} Учавствует в драке... И проигрывает! Показатели голода, жажды и чистоты СИЛЬНО повышены!");
		Thirsty += random.Next(5, 15);
		Hungry += random.Next(5, 15);
		Dirty += random.Next(5, 15);
	}
	private void Sing()
	{
		WriteMessageToConsole($"{Name} начинает читать Рэп! Показатели не изменились.");
		Thirsty -= random.Next(0, 0);
		Hungry -= random.Next(0, 0);
		Dirty -= random.Next(0, 0);
	}
	private void FightWin()
	{
		WriteMessageToConsole($"{Name} Учавствует в драке... И побеждает! Показатели голода, жажды и чистоты понижены!");
		Thirsty -= random.Next(5, 10);
		Hungry -= random.Next(5, 10);
		Dirty -= random.Next(5, 10);
	}
	private void Trance()
	{
		WriteMessageToConsole($"{Name} остужает свой пыл и впадает в транс. Показатели не изменились.");
		Thirsty += random.Next(0, 0);
		Hungry += random.Next(0, 0);
		Dirty += random.Next(0, 0);
	}
	private void Yell()
	{
		WriteMessageToConsole($"{Name} начинает истошно орать! Показатели не изменились.");
		Thirsty += random.Next(0, 0);
		Hungry += random.Next(0, 0);
		Dirty += random.Next(0, 0);
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
	internal void Clean()
	{
		Dirty -= random.Next(5, 10);
	}
	internal void Drink()
	{
		Thirsty -= random.Next(5, 10);
	}
	internal void GetGift()
	{
		WriteMessageToConsole($"{Name} получает подарок!");

		Gift tamagochina = new Tamagocha.Gift();

		int rnd = random.Next(0, 2);
		switch(rnd)
		{
			case 0:
				tamagochina.Open(this);
				break;
			case 1:
				tamagochina.Gnaw(this);
				break;
			case 2:
				tamagochina.Smash(this);
				break;
		}
	}

	public interface IPresent
	{
		void Open(Tamagocha tamagocha);
		void Gnaw(Tamagocha tamagocha);
		void Smash(Tamagocha tamagocha);
	}

	public class Gift : IPresent
	{
		public void Gnaw(Tamagocha tamagochina)
		{
		
			tamagochina.WriteMessageToConsole($"{tamagochina.Name} грызёт подарок!");
		}

		public void Open(Tamagocha tamagochina)
		{
			tamagochina.WriteMessageToConsole($"{tamagochina.Name} открывает подарок!");
		}

		public void Smash(Tamagocha tamagochina)
		{
			tamagochina.WriteMessageToConsole($"{tamagochina.Name} разносит подарок!");
		}
	}

}