using MFramework.Services.FakeData;
using System.Collections.Generic;

namespace Framework7SuperSimpleApp.Models
{
    public static class StaticDataSource
    {
        public static List<TodoItem> Items { get; set; }

        public static void Initialize()
        {
            Items = new List<TodoItem>();

            for (int i = 0; i < NumberData.GetNumber(10, 25); i++)
            {
                Items.Add(new TodoItem
                {
                    Id = i,
                    Text = TextData.GetSentence(),
                    IsHighPriority = BooleanData.GetBoolean(),
                    Status = EnumData.GetElement<TodoStatus>()
                });
            }
        }

    }

    public enum TodoStatus
    {
        todo = 0,
        done = 1,
        cancel = 2
    }

    public class TodoItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public TodoStatus Status { get; set; }
        public bool IsHighPriority { get; set; }
    }
}
