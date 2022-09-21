-- б) провести выборки клиентов, у которых сумма на счету ниже
-- определенного значения, отсортированных в порядке возрастания суммы;
select * from account
where amount > 150
order by amount;

-- в) провести поиск клиента с минимальной суммой на счете;
select min(a.amount), c.name
    from account a
join client c on c.id = a.id_client
group by c.name;

-- г) провести подсчет суммы денег у всех клиентов банка;
select  sum(amount) "Сумма всех денег у клиентов"
from account
join client c on account.id_client = c.id;

-- д) с помощью оператора Join, получить выборку банковских счетов и
-- их владельцев;
select currency_name, amount,surname,name,date_birth
from account
join client c on c.id = account.id_client;

-- е) вывести клиентов от самых старших к самым младшим;
select *
from client c
order by c.date_birth;

-- ж) подсчитать количество человек, у которых одинаковый возраст;
select date_birth,count(date_birth) "Количество"
from client c
group by date_birth;

-- з) сгруппировать клиентов банка по возрасту;
select date(date_birth)
from client c
group by date_birth;

-- и) вывести только N человек из таблицы.
select *
from client
limit 8