# Universal payments API .Net SDK

[![Build Status](https://travis-ci.org/QIWI-API/bill-payments-dotnet-sdk.svg?branch=master)](https://travis-ci.org/QIWI-API/bill-payments-dotnet-sdk)
[![Release Version](https://img.shields.io/nuget/v/Qiwi.BillPayments.svg)](https://www.nuget.org/packages/Qiwi.BillPayments/)
[![Downloads](https://img.shields.io/nuget/dt/Qiwi.BillPayments.svg)](https://www.nuget.org/packages/Qiwi.BillPayments/)

Библиотека .Net Core для внедрения единого платежного протокола эквайринга и QIWI Кошелька.

## Подключение

Установка с помощью Nuget:

```bash
nuget install Qiwi.BillPayments
```

## Документация

**API P2P-счетов (для физических лиц)**: https://developer.qiwi.com/ru/p2p-payments
**API QIWI Кассы (для юридических лиц)**: https://developer.qiwi.com/ru/bill-payments

## Авторизация

Для использования SDK требуется `secretKey`, подробности в [документации](https://developer.qiwi.com/ru/bill-payments/#auth).

```c#
var client = BillPaymentClientFactory.Create(
    secretKey: "eyJ2ZXJzaW9uIjoicmVzdF92MyIsImRhdGEiOnsibWVyY2hhbnRfaWQiOjUyNjgxMiwiYXBpX3VzZXJfaWQiOjcxNjI2MTk3LCJzZWNyZXQiOiJmZjBiZmJiM2UxYzc0MjY3YjIyZDIzOGYzMDBkNDhlYjhiNTnONPININONPN090MTg5Z**********************"
);
```

## Примеры

По-умолчанию пользователю доступно несколько способов оплаты.
В платежной форме параметры счета передаются в открытом виде в ссылке.
Далее клиенту отображается форма с выбором способа оплаты.
При использовании этого способа нельзя гарантировать, что все счета выставлены мерчантом, в отличие от выставления по API.
Через API все параметры передаются в закрытом виде, так же в API поддерживаются операции выставления и отмены счетов, возврата средств по оплаченным счетам, а также проверки статуса выполнения операций.

### Платежная форма

Простой способ для интеграции.
При открытии формы клиенту автоматически выставляется счет.
Параметры счета передаются в открытом виде в ссылке.
Далее клиенту отображается платежная форма с выбором способа оплаты.
При использовании этого способа нельзя гарантировать, что все счета выставлены мерчантом, в отличие от выставления по API.

Метод `CreatePaymentForm` создает платежную форму. В параметрах нужно указать:

* данные для создания платежной формы `paymentInfo`, включая:
  * ключ идентификации провайдера, полученный в QIWI Кассы, `PublicKey`;
  * идентификатор счета `BillId` внутри вашей системы;
  * сумму `Amount`;
  * не обязательный адрес перехода после успешной оплаты `SuccessUrl`;
* не обязательные дополнительные данные `customFields`, включая:
  * персонализация платежной формы `ThemeCode`.

В результате будет получена ссылка на форму оплаты, которую можно передать клиенту.
Подробнее о доступных параметрах в [документации](https://developer.qiwi.com/ru/bill-payments/#http).

```c#
client.CreatePaymentForm(
    paymentInfo: new PaymentInfo
    {
        PublicKey = "2tbp1WQvsgQeziGY9vTLe9vDZNg7tmCymb4Lh6STQokqKrpCC6qrUUKEDZAJ7mvFnzr1yTebUiQaBLDnebLMMxL8nc6FF5zfmGQnypdXCbQJqHEJW5RJmKfj8nvgc",
        Amount = new MoneyAmount
        {
            ValueDecimal = 499.9m,
            CurrencyEnum = CurrencyEnum.Rub
        },
        BillId = Guid.NewGuid().ToString(),
        SuccessUrl = "https://merchant.com/payment/success?billId=893794793973"
    },
    customFields: new CustomFields
    {
        ThemeCode = "кодСтиля"
    }
);
```

В результате:

```c#
new Uri(
    "https://oplata.qiwi.com/create?amount=499.90&customFields%5BapiClient%5D=dotnet_sdk&customFields%5BapiClientVersion%5D=0.1.0&customFields%5BthemeCode%5D=%D0%BA%D0%BE%D0%B4%D0%A1%D1%82%D0%B8%D0%BB%D1%8F&publicKey=2tbp1WQvsgQeziGY9vTLe9vDZNg7tmCymb4Lh6STQokqKrpCC6qrUUKEDZAJ11HeiD1GQX8jTnjMxLpMcSZuGZP7xbocwJicsoBAG1HyiPDJ8A8ecBKCKWu6FP5oa&billId=920dc584-ed30-4683-8251-486426768160&successUrl=https%3A%2F%2Fmerchant.com%2Fpayment%2Fsuccess%3FbillId%3D893794793973"
);
```

Возможные исключания: `UrlEncodingException`.

### Выставление счета

Надежный способ для интеграции.
Параметры передаются server2server с использованием авторизации.
Метод позволяет выставить счет, при успешном выполнении запроса в ответе вернется параметр `PayUrl` - ссылка для редиректа пользователя на платежную форму.

Метод `CreateBill` выставляет новый счет.
В параметрах нужно указать:

* данные для выставления стеча `info`, включая:
  * идентификатор счета `BillId` внутри вашей системы;
  * сумма счета `Amount`;
  * не обязательный комментарий `Comment`;
  * срок оплаты счета `ExpirationDateTime`;
  * информация о пользователе `Customer`;
  * не обязательный адрес перехода после успешной оплаты `SuccessUrl`;
* не обязательные дополнительные данные `customFields`, включая:
  * персонализация платежной формы `ThemeCode`.

В результате будет получен ответ с данными о выставленном счете.
Подробнее о доступных параметрах в [документации](https://developer.qiwi.com/ru/bill-payments/#create).

```c#
client.CreateBill(
    info: new CreateBillInfo
    {
        BillId = Guid.NewGuid().ToString(),
        Amount = new MoneyAmount
        {
            ValueDecimal = 199.9m,
            CurrencyEnum = CurrencyEnum.Rub
        },
        Comment = "comment",
        ExpirationDateTime = DateTime.Now.AddDays(45),
        Customer = new Customer
        {
            Email = "example@mail.org",
            Account = Guid.NewGuid().ToString(),
            Phone = "79123456789"
        },
        SuccessUrl = new Uri("http://merchant.ru/success")
    },
    customFields: new CustomFields
    {
        ThemeCode = "кодСтиля"
    }
);
```

Ответ:

```c#
new BillResponse
{
    SiteId = "270304",
    BillId = "81150938-dde5-45b8-ba22-df12cc6cee27",
    Amount = new MoneyAmount
    {
        ValueString = "199.90",
        CurrencyString = "RUB"
    },
    Status = new ResponseStatus
    {
        ValueString: "WAITING",
        ChangedDateTime: BillPaymentsUtils.ParseDate("2018-11-03T15:43:51+03:00")
    },
    Comment = "comment",
    Customer = new Customer
    {
        Email = "example@mail.org",
        Account = "040c3bb8-b207-4ecc-9ff9-90168d3bc34f",
        Phone = "79123456789"
    },
    CreationDateTime = BillPaymentsUtils.ParseDate("2018-11-03T15:43:51+03:00"),
    ExpirationDateTime = BillPaymentsUtils.ParseDate("2018-12-18T15:43:50+03:00"),
    PayUrl = new Uri("https://oplata.qiwi.com/form/?invoice_uid=c77a9051-1467-416b-991e-c25f06c61168&successUrl=http%3A%2F%2Fmerchant.ru%2Fsuccess"),
    CustomFields = new CustomFields
    {
        ApiClient = "dotnet_sdk",
        ApiClientVersion = "0.1.0",
        ThemeCode = "кодСтиля"
    }
};
```

Метод может быть вызван ассинхронно через `CreateBillAsync`.

Возможные исключания: `SerializationException`, `HttpClientException`, `BadResponseException`, `BillPaymentsServiceException`.

### Информация о счете

Метод `GetBillInfo` возвращает информацию о счете.
В параметрах нужно указать идентификатор счета `billId` внутри вашей системы, в результате будет получен ответ со статусом счета.
Подробнее в [документации](https://developer.qiwi.com/ru/bill-payments/#invoice-status).

```c#
client.GetBillInfo(
    billId: "fcb40a23-6733-4cf3-bacf-8e425fd1fc71"
);
```

Ответ:

```c#
new BillResponse
{
    SiteId = "270304",
    BillId = "fcb40a23-6733-4cf3-bacf-8e425fd1fc71",
    Amount = new MoneyAmount
    {
        ValueString = "199.90",
        CurrencyString = "RUB"
    },
    Status = new ResponseStatus
    {
        ValueString: "WAITING",
        ChangedDateTime: BillPaymentsUtils.ParseDate("2018-11-03T16:03:09+03:00")
    },
    Comment = "test",
    Customer = new Customer
    {
        Email = "example@mail.org",
        Account = "349d5978-bccc-4e10-be7e-3ca0808237b7",
        Phone = "79123456789"
    },
    CreationDateTime = BillPaymentsUtils.ParseDate("2018-11-03T16:03:09+03:00"),
    ExpirationDateTime = BillPaymentsUtils.ParseDate("2018-12-18T16:03:08+03:00"),
    PayUrl = new Uri("https://oplata.qiwi.com/form/?invoice_uid=b77618b4-746c-485f-8bb8-fff43ddef114"),
    CustomFields = new CustomFields
    {
        ApiClient = "dotnet_sdk",
        ApiClientVersion = "0.1.0"
    }
};
```

Метод может быть вызван ассинхронно через `GetBillInfoAsync`.

Возможные исключания: `SerializationException`, `HttpClientException`, `BadResponseException`, `BillPaymentsServiceException`.

### Отмена неоплаченного счета

Метод `CancelBill` отменяет неоплаченный счет.
В параметрах нужно указать идентификатор счета `billId` внутри вашей системы, в результате будет получен ответ с информацией о счете.
Подробнее в [документации](https://developer.qiwi.com/ru/bill-payments/#cancel).

```c#
client.CancelBill(
    billId: "fcb40a23-6733-4cf3-bacf-8e425fd1fc71"
);
```

Ответ:

```c#
new BillResponse
{
    SiteId = "270304",
    BillId = "fcb40a23-6733-4cf3-bacf-8e425fd1fc71",
    Amount = new MoneyAmount
    {
        ValueString = "199.90",
        CurrencyString = "RUB"
    },
    Status = new ResponseStatus
    {
        ValueString: "REJECTED",
        ChangedDateTime: BillPaymentsUtils.ParseDate("2018-11-03T16:03:09+03:00")
    },
    Comment = "test",
    Customer = new Customer
    {
        Email = "example@mail.org",
        Account = "349d5978-bccc-4e10-be7e-3ca0808237b7",
        Phone = "79123456789"
    },
    CreationDateTime = BillPaymentsUtils.ParseDate("2018-11-03T16:03:09+03:00"),
    ExpirationDateTime = BillPaymentsUtils.ParseDate("2018-12-18T16:03:08+03:00"),
    PayUrl = new Uri("https://oplata.qiwi.com/form/?invoice_uid=b77618b4-746c-485f-8bb8-fff43ddef114"),
    CustomFields = new CustomFields
    {
        ApiClient = "dotnet_sdk",
        ApiClientVersion = "0.1.0"
    }
};
```

Метод может быть вызван ассинхронно через `CancelBillAsync`.

Возможные исключания: `SerializationException`, `HttpClientException`, `BadResponseException`, `BillPaymentsServiceException`.

### Возврат средств
#### ! Метод недоступен для физических лиц

Метод `RefundBill` производит возврат средств.
В параметрах нужно указать:

* идентификатор счета `billId`;
* идентификатор возврата `refundId` внутри вашей системы;
* сумму возврата `amount`.

Подробнее в [документации](https://developer.qiwi.com/ru/bill-payments/#refund).

```c#
client.RefundBill(
    billId: "fcb40a23-6733-4cf3-bacf-8e425fd1fc71",
    refundId: Guid.NewGuid().ToString(),
    amount: new MoneyAmount
    {
        ValueDecimal = 104.9m,
        CurrencyEnum = CurrencyEnum.Rub
    }
);
```

В результате будет получен ответ c информацией о возврате:

```c#
new RefundResponse
{
    new MoneyAmount
    {
        ValueString = "104.90",
        CurrencyString = "RUB"
    },
    DateTime = BillPaymentsUtils.ParseDate("2018-11-03T16:11:57+03:00")
    RefundId = "3444e8ca-cf68-4dbd-92ee-f68c4bf8f29b",
    StatusString = "PARTIAL"
};
```

Метод может быть вызван ассинхронно через `RefundBillAsync`.

Возможные исключания: `SerializationException`, `HttpClientException`, `BadResponseException`, `BillPaymentsServiceException`.

### Информация о возврате
#### ! Метод недоступен для физических лиц

Метод `GetRefundInfo` запрашивает статус возврата, в параметрах нужно указать:

* идентификатор счета `billId`;
* идентификатор возврата `refundId` внутри вашей системы.

Подробнее в [документации](https://developer.qiwi.com/ru/bill-payments/#refund-status).

```c#
client.GetRefundInfo(
    billId: "fcb40a23-6733-4cf3-bacf-8e425fd1fc71",
    refundId: "3444e8ca-cf68-4dbd-92ee-f68c4bf8f29b"
);
```

В результате будет получен ответ c информацией о возврате:

```c#
new RefundResponse
{
    Amount = new MoneyAmount
    {
        ValueString = "104.90",
        CurrencyString = "RUB"
    },
    DateTime = BillPaymentsUtils.ParseDate("2018-11-03T16:11:57+03:00")
    RefundId = "3444e8ca-cf68-4dbd-92ee-f68c4bf8f29b",
    StatusString = "PARTIAL"
};
```

Метод может быть вызван ассинхронно через `GetRefundInfoAsync`.

Возможные исключания: `SerializationException`, `HttpClientException`, `BadResponseException`, `BillPaymentsServiceException`.

### Вспомогательные методы

Метод `CheckNotificationSignature` осуществляет проверку подписи при нотификации о новом счете от сервера уведомлений QIWI.
Принимает на вход подпись из входящего запроса, объект - тело запроса и secret ключ, с помощью которого должна осуществляться подпись:

```c#
Assert.IsTrue(
    condition: BillPaymentsUtils.CheckNotificationSignature(
        validSignature: "07e0ebb10916d97760c196034105d010607a6c6b7d72bfa1c3451448ac484a3b",
        notification: new Notification
        {
            Bill = new Bill
            {
                SiteId = "test",
                BillId = "test_bill",
                Amount = new MoneyAmount
                {
                    ValueDecimal = 1m,
                    CurrencyEnum = CurrencyEnum.Rub
                },
                Status = new BillStatus
                {
                    ValueEnum = BillStatusEnum.Paid
                }
            ),
            Version = "1"
        },
        merchantSecret: "test-merchant-secret-for-signature-check"
    )
);
```

Возможные исключания: `EncryptionException`.

### Использование альтернативного обработчика JSON

По умолчанию для работы с JSON используется `System.Runtime.Serialization.Json.DataContractJsonSerializer`, но можно воспользоваться любым другим, реализовав интерфейс `IObjectMapper`.

```c#
public interface IClient {
    string WriteValue(object entityOpt);
    T ReadValue<T>(string body);
}
```

Примером реализации является `ContractObjectMapper`, принимающий `System.Runtime.Serialization.Json.DataContractJsonSerializerSettings` для конфигурации совместимости формата дат.

```c#
BillPaymentClientFactory.Create(
    secretKey: "eyJ2ZXJzaW9uIjoicmVzdF92MyIsImRhdGEiOnsibWVyY2hhbnRfaWQiOjUyNjgxMiwiYXBpX3VzZXJfaWQiOjcxNjI2MTk3LCJzZWNyZXQiOiJmZjBiZmJiM2UxYzc0MjY3YjIyZDIzOGYzMDBkNDhlYjhiNTnONPININONPN090MTg5Z**********************",
    client: null,
    objectMapper: ObjectMapperFactory.Create<ContractObjectMapper>(
        settings: new DataContractJsonSerializerSettings
        {
            DateTimeFormat = new DateTimeFormat(BillPaymentsClient.DateTimeFormat)
        }
    )
);
```

Проект **Qiwi.BillPayments.Json.Newtonsoft** предоставляет обработчик JSON на освнове пакета [Newtonsoft.Json](https://www.newtonsoft.com/json).

### Использование альтернативного HTTP-клиента

По умолчанию для отправки HTTP-запросов используется `System.Net.Http.HttpClient`, но можно воспользоваться любым другим, реализовав интерфейс `IClient`.

```c#
public interface IClient {
    ResponseData Request(
        string method,
        string url,
        IReadOnlyDictionary<string, string> headers,
        string entityOpt = null
    );
    Task<ResponseData> RequestAsync(
        string method,
        string url,
        IReadOnlyDictionary<string, string> headers,
        string entityOpt = null
    );
}
```

Примером реализации является `NetClient`, принимающий `System.Net.Http.HttpClient` произвольной конфигурации.

```c#
BillPaymentClientFactory.Create(
    secretKey: "eyJ2ZXJzaW9uIjoicmVzdF92MyIsImRhdGEiOnsibWVyY2hhbnRfaWQiOjUyNjgxMiwiYXBpX3VzZXJfaWQiOjcxNjI2MTk3LCJzZWNyZXQiOiJmZjBiZmJiM2UxYzc0MjY3YjIyZDIzOGYzMDBkNDhlYjhiNTnONPININONPN090MTg5Z**********************",
    client: ClientFactory.Create<NetClient>(
        httpClient: new HttpClient(
            handler: new HttpClientHandler()
            {
                Proxy: new WebProxy("http://proxyserver:80/", true)
            }
        )
    )
);
```

## Работа с ошибками

Данная библиотека не берет на себя обработку исключительных ситуаций, при ее использовании следует определить и перехватывать возможные исключительные ситуации самостоятельно.
При использовании SDK в нештатных ситуациях могут возникать следующие исключения из пространства имен `Qiwi.BillPayments.Exception`:

### `BadResponseException`

Возникает, если ответ сервера получен, но не содержит корректный JSON.
Обьект сообщения типа `ResponseData` можно получить из поля `Response`.
Код ответа HTTP можно получить из поля `HttpStatus`.

### `BillPaymentsServiceException`

Возникает, если от API получено сообщение об ошибке.
Обьект сообщения типа `ErrorResponse` можно получить из поля `Response`.
Код ответа HTTP можно получить из поля `HttpStatus`.

### `EncryptionException`

Возникает, если невозможно получить хэш строки, алгоритм не поддерживается или не задан секретный ключ.
Содержит сообщение и строит стек вызовов от оригинальной ошибки.

### `HttpClientException`

Возникает, если невозможно выполнить запрос к API, ошибка HTTP клиента.
Содержит сообщение и строит стек вызовов от оригинальной ошибки.

### `SerializationException`

Возникает, если передаваемые в API данные не могут быть корректно сериализованы в JSON.
Содержит сообщение и строит стек вызовов от оригинальной ошибки.

### `UrlEncodingException`

Возникает, если невозможно сформировать корректный URL.
Содержит сообщение и строит стек вызовов от оригинальной ошибки.

### TRY...CATCH

Запросы выполняются в отдельном потоке, даже если используется синхронная форма вызова метода.
Таким образом, перехват исключений должен использовать класс `AggregateException` для обьекта ошибки.

```C#
try
{
    client.CreateBill(billInfo);
}
catch (AggregateException aggregateException)
{
    var exception = exception.GetBaseException();
    // exception - исходная ошибка, которую следует обработать
}
```

## Требования

* **.Net Standart 2.0** или **.Net Core 2.0** или **.Net Framework 4.5**

## Лицензия

[MIT](LICENSE)
