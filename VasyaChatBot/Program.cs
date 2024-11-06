using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotVasya
{
    class Program
    {
        // настройка бота
        static void Main(string[] args)
        {
            var client = new TelegramBotClient("YOUR_TOKEN");
            client.StartReceiving(Update, Error);
            Console.ReadLine(); // в будущем он нам понадобиться для взаимодействия через командную строку с пользователем        }

            // настройка функционала
            async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
            {
                var app = botClient;
                var message = update.Message;
                var mCI = message == null ? 0 : message.Chat.Id;
                if (message.Text != null)
                {
                    Console.WriteLine($"{message.From.FirstName} | {message.Text}");
                    switch (message.Text.ToLower())
                    {
                        case "привет":
                            await app.SendMessage(mCI, "Hi!");
                            break;
                        case "9":
                            await app.SendMessage(mCI, "=)");
                            break;
                        case "сука": //Осуждаем сквернославие, поэтому удаляем сообщение
                            await app.DeleteMessage(mCI, message.MessageId);
                            break;
                    }
                } 
                // Удаляем фотографию или документ

                if (message.Photo != null || message.Document != null)
                {
                    await app.DeleteMessage(mCI, message.MessageId);
                }

            }
        }

        // Отлов ошибок
        private static async Task Error(ITelegramBotClient argum1, Exception argum2, CancellationToken argum3)
        {
            //Console.WriteLine(argum1 + "\n" + argum2); 
            throw new NotImplementedException();
        }
    }
}