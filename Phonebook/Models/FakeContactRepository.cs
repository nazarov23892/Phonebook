using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public class FakeContactRepository : IContactRepository
    {
        private List<Contact> contacts = new List<Contact>();

        public IEnumerable<Contact> Persons 
        {
            get => contacts;
        }

        public FakeContactRepository()
        {
            contacts.AddRange(GetContacts());
        }

        public int AddContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public void DeleteContact(int contactId)
        {
            throw new NotImplementedException();
        }

        public void SaveContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        private Contact[] GetContacts()
        {
            Contact[] persons = new []
            {
                new Contact
                {
                    ContactId = 1,
                    Firstname = "Аксёнов",
                    Lastname = "Оливер",
                    Patronymic = "Леонидович"
                },
                new Contact
                {
                    ContactId = 2,
                    Firstname = "Силин",
                    Lastname = "Прохор",
                    Patronymic = "Борисович"
                },
                new Contact
                {
                    ContactId = 3,
                    Firstname = "Горбунов",
                    Lastname = "Спартак",
                    Patronymic = "Петрович"
                },
                new Contact
                {
                    ContactId = 4,
                    Firstname = "Воронцов",
                    Lastname = "Шерлок",
                    Patronymic = "Данилович"
                },
                new Contact
                {
                    ContactId = 5,
                    Firstname = "Павленко",
                    Lastname = "Эдуард",
                    Patronymic = "Алексеевич"
                },
                new Contact
                {
                    ContactId = 6,
                    Firstname = "Ларионов",
                    Lastname = "Добрыня",
                    Patronymic = "Станиславович"
                },
                new Contact
                {
                    ContactId = 7,
                    Firstname = "Селезнёв",
                    Lastname = "Йозеф",
                    Patronymic = "Владимирович"
                },
                new Contact
                {
                    ContactId = 8,
                    Firstname = "Веселов",
                    Lastname = "Клаус",
                    Patronymic = "Григорьевич"
                },
                new Contact
                {
                    ContactId = 9,
                    Firstname = "Иванов",
                    Lastname = "Савва",
                    Patronymic = "Вадимович"
                },
                new Contact
                {
                    ContactId = 10,
                    Firstname = "Семёнов",
                    Lastname = "Орест",
                    Patronymic = "Александрович"
                },
                new Contact
                {
                    ContactId = 11,
                    Firstname = "Нестеров",
                    Lastname = "Виталий",
                    Patronymic = "Ярославович"
                },
                new Contact
                {
                    ContactId = 12,
                    Firstname = "Хованский",
                    Lastname = "Тимур",
                    Patronymic = "Сергеевич"
                },
                new Contact
                {
                    ContactId = 13,
                    Firstname = "Иванив",
                    Lastname = "Цицерон",
                    Patronymic = "Борисович"
                },
                new Contact
                {
                    ContactId = 14,
                    Firstname = "Петрик",
                    Lastname = "Юрий",
                    Patronymic = "Петрович"
                },
                new Contact
                {
                    ContactId = 15,
                    Firstname = "Рябов",
                    Lastname = "Лука",
                    Patronymic = "Александрович"
                },
                new Contact
                {
                    ContactId = 16,
                    Firstname = "Герасимов",
                    Lastname = "Гарри",
                    Patronymic = "Иванович"
                },
                new Contact
                {
                    ContactId = 17,
                    Firstname = "Коцюбинский",
                    Lastname = "Евстахий",
                    Patronymic = "Богданович"
                },
                new Contact
                {
                    ContactId = 18,
                    Firstname = "Бондаренко",
                    Lastname = "Чарльз",
                    Patronymic = "Львович"
                },
                new Contact
                {
                    ContactId = 19,
                    Firstname = "Кононов",
                    Lastname = "Влад",
                    Patronymic = "Брониславович"
                },
                new Contact
                {
                    ContactId = 20,
                    Firstname = "Кобзар",
                    Lastname = "Данила",
                    Patronymic = "Александрович"
                },
                new Contact
                {
                    ContactId = 21,
                    Firstname = "Игнатов",
                    Lastname = "Ираклий",
                    Patronymic = "Григорьевич"
                },
                new Contact
                {
                    ContactId = 22,
                    Firstname = "Плаксий",
                    Lastname = "Яромир",
                    Patronymic = "Ярославович"
                },
                new Contact
                {
                    ContactId = 23,
                    Firstname = "Иващенко",
                    Lastname = "Вениамин",
                    Patronymic = "Леонидович"
                },
                new Contact
                {
                    ContactId = 24,
                    Firstname = "Никонов",
                    Lastname = "Йомер",
                    Patronymic = "Михайлович"
                },
                new Contact
                {
                    ContactId = 25,
                    Firstname = "Михеев",
                    Lastname = "Леон",
                    Patronymic = "Сергеевич"
                },
                new Contact
                {
                    ContactId = 26,
                    Firstname = "Карпов",
                    Lastname = "Василий",
                    Patronymic = "Богданович"
                },
                new Contact
                {
                    ContactId = 27,
                    Firstname = "Коцюбинский",
                    Lastname = "Камиль",
                    Patronymic = "Иванович"
                },
                new Contact
                {
                    ContactId = 28,
                    Firstname = "Карпенко-Карый",
                    Lastname = "Устин",
                    Patronymic = "Максимович"
                },
                new Contact
                {
                    ContactId = 29,
                    Firstname = "Петренко",
                    Lastname = "Устин",
                    Patronymic = "Артёмович"
                },
                new Contact
                {
                    ContactId = 30,
                    Firstname = "Плаксий",
                    Lastname = "Устин",
                    Patronymic = "Иванович"
                },
                new Contact
                {
                    ContactId = 31,
                    Firstname = "Калашников",
                    Lastname = "Назар",
                    Patronymic = "Ярославович"
                },
                new Contact
                {
                    ContactId = 32,
                    Firstname = "Туров",
                    Lastname = "Жерар",
                    Patronymic = "Викторович"
                },
                new Contact
                {
                    ContactId = 33,
                    Firstname = "Васильев",
                    Lastname = "Антонин",
                    Patronymic = "Максимович"
                },
                new Contact
                {
                    ContactId = 34,
                    Firstname = "Власов",
                    Lastname = "Евсей",
                    Patronymic = "Петрович"
                },
                new Contact
                {
                    ContactId = 35,
                    Firstname = "Житар",
                    Lastname = "Герасим",
                    Patronymic = "Анатолиевич"
                },
                new Contact
                {
                    ContactId = 36,
                    Firstname = "Кулишенко",
                    Lastname = "Леопольд",
                    Patronymic = "Григорьевич"
                },
                new Contact
                {
                    ContactId = 37,
                    Firstname = "Дидовец",
                    Lastname = "Павел",
                    Patronymic = "Петрович"
                },
                new Contact
                {
                    ContactId = 38,
                    Firstname = "Терещенко",
                    Lastname = "Руслан",
                    Patronymic = "Брониславович"
                },
                new Contact
                {
                    ContactId = 39,
                    Firstname = "Лыткин",
                    Lastname = "Цицерон",
                    Patronymic = "Михайлович"
                },
                new Contact
                {
                    ContactId = 40,
                    Firstname = "Моисеев",
                    Lastname = "Никодим",
                    Patronymic = "Алексеевич"
                },
                new Contact
                {
                    ContactId = 41,
                    Firstname = "Королёв",
                    Lastname = "Спартак",
                    Patronymic = "Александрович"
                },
                new Contact
                {
                    ContactId = 42,
                    Firstname = "Навальный",
                    Lastname = "Эдуард",
                    Patronymic = "Романович"
                },
                new Contact
                {
                    ContactId = 43,
                    Firstname = "Попов",
                    Lastname = "Шарль",
                    Patronymic = "Виталиевич"
                },
                new Contact
                {
                    ContactId = 44,
                    Firstname = "Пономарёв",
                    Lastname = "Ростислав",
                    Patronymic = "Брониславович"
                },
                new Contact
                {
                    ContactId = 45,
                    Firstname = "Шилов",
                    Lastname = "Виктор",
                    Patronymic = "Брониславович"
                },
                new Contact
                {
                    ContactId = 46,
                    Firstname = "Бутко",
                    Lastname = "Харитон",
                    Patronymic = "Петрович"
                },
                new Contact
                {
                    ContactId = 47,
                    Firstname = "Горобчук",
                    Lastname = "Геннадий",
                    Patronymic = "Ярославович"
                },
                new Contact
                {
                    ContactId = 48,
                    Firstname = "Третьяков",
                    Lastname = "Феликс",
                    Patronymic = "Юхимович"
                },
                new Contact
                {
                    ContactId = 49,
                    Firstname = "Ефремов",
                    Lastname = "Богдан",
                    Patronymic = "Богданович"
                },
                new Contact
                {
                    ContactId = 50,
                    Firstname = "Щукин",
                    Lastname = "Зенон",
                    Patronymic = "Брониславович"
                },
                new Contact
                {
                    ContactId = 51,
                    Firstname = "Петровский",
                    Lastname = "Оскар",
                    Patronymic = "Данилович"
                },
                new Contact
                {
                    ContactId = 52,
                    Firstname = "Вальковский",
                    Lastname = "Кир",
                    Patronymic = "Петрович"
                },
                new Contact
                {
                    ContactId = 53,
                    Firstname = "Чухрай",
                    Lastname = "Тит",
                    Patronymic = "Владимирович"
                },
                new Contact
                {
                    ContactId = 54,
                    Firstname = "Хижняк",
                    Lastname = "Сава",
                    Patronymic = "Ярославович"
                },
                new Contact
                {
                    ContactId = 55,
                    Firstname = "Зимин",
                    Lastname = "Константин",
                    Patronymic = "Александрович"
                },
                new Contact
                {
                    ContactId = 56,
                    Firstname = "Тарасюк",
                    Lastname = "Йосып",
                    Patronymic = "Михайлович"
                },
                new Contact
                {
                    ContactId = 57,
                    Firstname = "Герасимов",
                    Lastname = "Йозеф",
                    Patronymic = "Александрович"
                },
                new Contact
                {
                    ContactId = 58,
                    Firstname = "Рыбаков",
                    Lastname = "Витольд",
                    Patronymic = "Станиславович"
                },
                new Contact
                {
                    ContactId = 59,
                    Firstname = "Бобылёв",
                    Lastname = "Прохор",
                    Patronymic = "Григорьевич"
                },
                new Contact
                {
                    ContactId = 60,
                    Firstname = "Притула",
                    Lastname = "Пётр",
                    Patronymic = "Андреевич"
                },
                new Contact
                {
                    ContactId = 61,
                    Firstname = "Притула",
                    Lastname = "Йоган",
                    Patronymic = "Брониславович"
                },
                new Contact
                {
                    ContactId = 62,
                    Firstname = "Горбачёв",
                    Lastname = "Захар",
                    Patronymic = "Юхимович"
                },
                new Contact
                {
                    ContactId = 63,
                    Firstname = "Саксаганский",
                    Lastname = "Вадим",
                    Patronymic = "Дмитриевич"
                },
                new Contact
                {
                    ContactId = 64,
                    Firstname = "Котовский",
                    Lastname = "Харитон",
                    Patronymic = "Валериевич"
                }
                
            };
            return persons;
        }
    }
}
