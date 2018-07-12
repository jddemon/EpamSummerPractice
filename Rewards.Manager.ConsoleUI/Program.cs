using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Rewards.Manager.LogicContracts;
using Rewards.Manager.NewConfig;
using Rewards.Manager.Entities;

namespace Rewards.Manager.ConsoleUI
{
    class Program
    {
        // сервера зависимости
        private static IRewardLogic rewardLogic;
        private static IPersonLogic personLogic;
        private static IMedalLogic medalLogic;

        static Program()
        {
            // запуск ninject
            IKernel ninjectKernel = new StandardKernel();
            // регистрация серверов
            Config.RegisterServices(ninjectKernel);
            // создание связанных элементов
            rewardLogic = ninjectKernel.Get<IRewardLogic>();
            personLogic = ninjectKernel.Get<IPersonLogic>();
            medalLogic = ninjectKernel.Get<IMedalLogic>();
        }

        static string[] menutext = { "1.Список людей\n"
                                    +"2.Добавить человека в список\n"
                                    +"3.Удалить человека из списка\n"
                                    +"4.Внести изменения по человеку\n"
                                    +"5.Список наград у человека\n"
                                    +"6.Добавить награду\n"
                                    +"7.Список всех видов наград\n"
                                    +"8.Новый вид награды\n"
                                    +"9.Удалить вид награды\n"
                                    +"0.Выход\n"
        };

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine(menutext[0]);
                    ConsoleKeyInfo entry = Console.ReadKey(intercept: true);
                    switch (entry.Key)
                    {
                        case ConsoleKey.D1:
                            ListPerson();
                            break;

                        case ConsoleKey.D2:
                            AddPerson();
                            break;
                           
                        case ConsoleKey.D3:
                            DeletePerson();
                            break;
                        case ConsoleKey.D4:
                            UpdatePerson();
                            break;
                            
                        case ConsoleKey.D5:
                            ListPersonReward();
                            break;
                           
                        case ConsoleKey.D6:
                            RewardPerson();
                            break;
                            
                        case ConsoleKey.D7:
                            ListMedal();
                            break;
                        case ConsoleKey.D8:
                            AddMedal();
                            break;
                            
                        case ConsoleKey.D9:
                            DeleteMedal();
                            break;
                            
                        case ConsoleKey.D0:
                            return;
                        default:
                            break;
                    }
                    Prompt("Нажмите Enter");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                    Console.ReadLine();
                }
            }
        }


        // функция меню вывода списка людей
        static void ListPerson() {
            Person[] persons = personLogic.GetAll();
            foreach (var person in persons)
            {
                Console.WriteLine($"{person.id}. {person.Imja} {person.Familia}");
            }
        }

        // функция выводит текст и ожидает ввода строки 
        static string Prompt(string text)
        {
            Console.WriteLine(text);
            string rez = Console.ReadLine();
            return rez;
        }

        // ввод информации о человеке
        static Person input()
        {
            Person person = new Person();
            person.Familia = Prompt("Фамилия ?");
            person.Imja = Prompt("Имя ?");
            person.Adres = Prompt("Адрес(Город,улица,дом) ?");
            int Year = int.Parse(Prompt("Год рождения ?"));
            int Month = int.Parse(Prompt("Месяц рождения ?"));
            int Day = int.Parse(Prompt("Число(день) рождения ?"));
            person.Birthday = new DateTime(Year, Month, Day);
            person.Vozrast = int.Parse(Prompt("Возраст ?"));
            return person;
        }


        // функция меню добавления человека
        static void AddPerson()
        {
            personLogic.Save(input());
        }

        // функция меню удаление из списка
        static void DeletePerson()
        {
            ListPerson();
            int id = int.Parse(Prompt("Введите id удаляемого:"));
            if(personLogic.Delete(id))
                Console.WriteLine(id.ToString() + " удален.");
            else
                Console.WriteLine(id.ToString() + " не найден.");
        }

        // функция меню обновление человека
        static void UpdatePerson()
        {
            ListPerson();
            int id = int.Parse(Prompt("Введите id человека для изменений:"));
            Person person=personLogic.GetById(id);
            if (person == null)
            {
                Console.WriteLine(id.ToString() + " не найден.");
                return;
            }

            Console.WriteLine($"{person.id}. {person.Imja} {person.Familia} {person.Birthday} {person.Vozrast} {person.Adres}");
            person = input();
            person.id = id;
            person = personLogic.Update(person);
            if (person != null)
            {
                Console.WriteLine($"Новые сведения\n{person.id}. {person.Imja} {person.Familia} {person.Birthday} {person.Vozrast} {person.Adres}");
                Console.WriteLine(id.ToString() + " изменен.");
            }
            else
                Console.WriteLine(id.ToString() + " измененить не удалось.");
        }

        // функция меню список награжденных
        static void ListPersonReward()
        {
            ListPerson();
            int id = int.Parse(Prompt("Введите id для просмотра списка наград:"));
            Person person = personLogic.GetById(id);
            if (person == null)
            {
                Console.WriteLine(id.ToString() + " не найден.");
                return;
            }

            Reward[] rewards = rewardLogic.GetPersonAll(person.id);
            foreach (var reward in rewards)
            {
                Console.WriteLine($"{reward.awarded.Imja} {person.Familia} награжден {reward.medal.Name} из {reward.medal.Material}  ");
            }
        }

        // функция меню награждение из списка
        static void RewardPerson()
        {
            ListPerson();
            int id = int.Parse(Prompt("Введите id для награждения:"));
            Person person = personLogic.GetById(id);
            if (person == null)
            {
                Console.WriteLine(id.ToString() +" не наден.");
                return;
            }

            ListMedal();
            int mid = int.Parse(Prompt("Введите номер медали:"));

            Medal medal = medalLogic.GetById(mid);
            if (medal == null)
            {
                Console.WriteLine("Не надена медаль.");
                return;
            }
            Reward reward = new Reward();
            reward.awarded = person;
            reward.medal = medal;
            reward=rewardLogic.Save(reward);
            if (reward != null)

                Console.WriteLine("Добавлена награда");
            else
                Console.WriteLine("Ошибка награждения");

        }

        //функция меню список медалей
        static void ListMedal()
        {
            Medal[] medals = medalLogic.GetAll();
            foreach (var medal in medals)
            {
                Console.WriteLine($"{medal.id}. {medal.Name} {medal.Material}");
            }
        }

        // функция меню добавление медали
        static void AddMedal()
        {
            Medal medal = new Medal();
            medal.Name = Prompt("Название медали ?");
            medal.Material = Prompt("Из какого материала ?");
            medalLogic.Save(medal);
            Console.WriteLine($"{medal.id}. {medal.Name} {medal.Material} добавлена");
        }

        // функция меню удаление медали
        static void DeleteMedal()
        {
            ListMedal();
            int id = int.Parse(Prompt("Введите номер медали:"));
            Medal medal = medalLogic.GetById(id);
            if(  medalLogic.Delete(id) )
            { 
                Console.WriteLine("Медаль "+medal.Name+"удалена.");
                    return;
            }               
            else
                Console.WriteLine("Сбой удаления медали.");
        }
    }
}
