using System.Diagnostics;
using Services;


var testData = new TestDataGenerator();
var sw = new Stopwatch();
int countElementsInCollection = 1000000;
var listClient = testData.GenerateListClient(countElementsInCollection);
var dictionaryClient = testData.GenerateDictionaryClient(countElementsInCollection);
var listEmployee = testData.GenerateListEmployee(countElementsInCollection);
var dateCompare = DateTime.Now.AddYears(-1);
int timePhoneSearchList = 0;
int timePhoneSearchDictionary = 0;
int ageBelow = 0;
int searchMinSalary = 0;
int speedWorkFirstOrDefault = 0;
int speedWorkOnKey = 0;


for (int i = 0; i < 10; i++)
{
    sw.Start();
    listClient.Find(c => c.NumberPhone == 323232);
    sw.Stop();
    timePhoneSearchList += sw.Elapsed.Milliseconds;
    
    sw.Restart();
    dictionaryClient.ContainsKey(111234);
    sw.Stop();
    timePhoneSearchDictionary += sw.Elapsed.Milliseconds;
    
    sw.Restart();
    listClient.FindAll(c => c.DateBirth < dateCompare);
    sw.Stop();
    ageBelow += sw.Elapsed.Milliseconds;
    
    sw.Restart();
    listEmployee.Min(e => e.Salary);
    sw.Stop();
    searchMinSalary += sw.Elapsed.Milliseconds;
    
    sw.Restart();
    dictionaryClient.FirstOrDefault(c => c.Key == countElementsInCollection-1);
    sw.Stop();
    speedWorkFirstOrDefault += sw.Elapsed.Milliseconds;
    
    sw.Restart();
    var client = dictionaryClient[countElementsInCollection-1];
    sw.Stop();
    speedWorkOnKey += sw.Elapsed.Milliseconds;
    
    sw.Reset();
    
}

Console.WriteLine($"a)Поиск по номеру телефона клиента в списке: {timePhoneSearchList/10}\n" +
                  $"б)Поиск по номеру телефона клиента в словаре: {timePhoneSearchDictionary/10}\n" +
                  $"в)Выборка клиентов по критерию возраста: {ageBelow/10}\n" +
                  $"г)Поиск сотрудника с минимальнйо оплатой: {searchMinSalary/10}\n" +
                  $"д)1)Поиск в словаре с помощью FirstOrDefault: {speedWorkFirstOrDefault/10}\n" +
                  $"д)2)Поиск в словаре с помощью ключа: {speedWorkOnKey/10}\n");


Console.WriteLine();
