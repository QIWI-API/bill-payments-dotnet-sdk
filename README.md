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

**Универсальный платежный API**: https://developer.qiwi.com/ru/bill-payments

## Авторизация

Для использования SDK требуется `secretKey`, подробности в [документации](https://developer.qiwi.com/ru/bill-payments/#auth).

```c#
var secretKey = "eyJ2ZXJzaW9uIjoicmVzdF92MyIsImRhdGEiOnsibWVyY2hhbnRfaWQiOjUyNjgxMiwiYXBpX3VzZXJfaWQiOjcxNjI2MTk3LCJzZWNyZXQiOiJmZjBiZmJiM2UxYzc0MjY3YjIyZDIzOGYzMDBkNDhlYjhiNTnONPININONPN090MTg5Z**********************";

var client = BillPaymentClientFactory.createDefault(secretKey);
```

## Примеры

По-умолчанию пользователю доступно несколько способов оплаты. В платежной форме параметры счета передаются в открытом виде в ссылке. Далее клиенту отображается форма с выбором способа оплаты. При использовании этого способа нельзя гарантировать, что все счета выставлены мерчантом, в отличие от выставления по API.
Через API все параметры передаются в закрытом виде, так же в API поддерживаются операции выставления и отмены счетов, возврата средств по оплаченным счетам, а также проверки статуса выполнения операций.


### Платежная форма

Простой способ для интеграции. При открытии формы клиенту автоматически выставляется счет. Параметры счета передаются в открытом виде в ссылке. Далее клиенту отображается платежная форма с выбором способа оплаты. При использовании этого способа нельзя гарантировать, что все счета выставлены мерчантом, в отличие от выставления по API.

Метод `createPaymentForm` создает платежную форму. В параметрах нужно указать:
* ключ идентификации провайдера, полученный в QIWI Кассе, `publicKey`;
* идентификатор счета `billId` внутри вашей системы;
* сумму `amount`;
* адрес перехода после успешной оплаты `successUrl`.

В результате будет получена ссылка на форму оплаты, которую можно передать клиенту.
Подробнее о доступных параметрах в [документации](https://developer.qiwi.com/ru/bill-payments/#http).

```c#
var publicKey = "2tbp1WQvsgQeziGY9vTLe9vDZNg7tmCymb4Lh6STQokqKrpCC6qrUUKEDZAJ7mvFnzr1yTebUiQaBLDnebLMMxL8nc6FF5zfmGQnypdXCbQJqHEJW5RJmKfj8nvgc";

var amount = new MoneyAmount
{
    ValueDecimal = 499.9m,
    CurrencyEnum = CurrencyEnum.Rub
};
var billId = Guid.NewGuid().ToString();
var successUrl = "https://merchant.com/payment/success?billId=893794793973";

var paymentUrl = client.createPaymentForm(new PaymentInfo(key, amount, billId, successUrl));
```

В результате:

```
https://oplata.qiwi.com/create?amount=499.90&customFields%5BapiClient%5D=java_sdk&customFields%5BapiClientVersion%5D=1.0.0&publicKey=2tbp1WQvsgQeziGY9vTLe9vDZNg7tmCymb4Lh6STQokqKrpCC6qrUUKEDZAJ11HeiD1GQX8jTnjMxLpMcSZuGZP7xbocwJicsoBAG1HyiPDJ8A8ecBKCKWu6FP5oa&billId=920dc584-ed30-4683-8251-486426768160&successUrl=https%3A%2F%2Fmerchant.com%2Fpayment%2Fsuccess%3FbillId%3D893794793973
```

### Выставление счета

Надежный способ для интеграции. Параметры передаются server2server с использованием авторизации. Метод позволяет выставить счет, при успешном выполнении запроса в ответе вернется параметр `payUrl` - ссылка для редиректа пользователя на платежную форму.

Метод `createBill` выставляет новый счет. В параметрах нужно указать:
* идентификатор счета `billId` внутри вашей системы;
* сумма счета `amount`;
* комментарий `comment`;
* срок оплаты счета `expirationDateTime` (тип `ZonedDateTime`);
* информация о пользователе `customer`;
* адрес перехода после успешной оплаты `successUrl`.

В результате будет получен ответ с данными о выставленном счете.
Подробнее о доступных параметрах в [документации](https://developer.qiwi.com/ru/bill-payments/#create).

```c#
var billInfo = new CreateBillInfo
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
};
var response = client.createBill(billInfo);
```

Ответ:

```json
{
    "siteId": "270304",
    "billId": "81150938-dde5-45b8-ba22-df12cc6cee27",
    "amount": {
        "value": "199.90",
        "currency": "RUB"
    },
    "status": {
        "value": "WAITING",
        "changedDateTime": "2018-11-03T15:43:51.407Z"
    },
    "comment": "comment",
    "customer": {
        "email": "example@mail.org",
        "account": "040c3bb8-b207-4ecc-9ff9-90168d3bc34f",
        "phone": "79123456789"
    },
    "creationDateTime": "2018-11-03T15:43:51.407Z",
    "expirationDateTime": "2018-12-18T15:43:50.951Z",
    "payUrl": "https://oplata.qiwi.com/form/?invoice_uid=c77a9051-1467-416b-991e-c25f06c61168&successUrl=http%3A%2F%2Fmerchant.ru%2Fsuccess",
    "customFields": {
        "apiClient": "dotnet_sdk",
        "apiClientVersion": "0.0.1"
    }
}
```

### Информация о счете

Метод `getBillInfo` возвращает информацию о счете.
В параметрах нужно указать идентификатор счета `billId` внутри вашей системы, в результате будет получен ответ со статусом счета.
Подробнее в [документации](https://developer.qiwi.com/ru/bill-payments/#invoice-status).

```c#
var billId = "fcb40a23-6733-4cf3-bacf-8e425fd1fc71";

var response = client.getBillInfo(billId);
```

Ответ:

```json
{
    "siteId": "270304",
    "billId": "fcb40a23-6733-4cf3-bacf-8e425fd1fc71",
    "amount": {
        "value": "199.90",
        "currency": "RUB"
    },
    "status": {
        "value": "WAITING",
        "changedDateTime": "2018-11-03T16:03:09.062Z"
    },
    "comment": "test",
    "customer": {
        "email": "example@mail.org",
        "account": "349d5978-bccc-4e10-be7e-3ca0808237b7",
        "phone": "79123456789"
    },
    "creationDateTime": "2018-11-03T16:03:09.062Z",
    "expirationDateTime": "2018-12-18T16:03:08.668Z",
    "payUrl": "https://oplata.qiwi.com/form/?invoice_uid=b77618b4-746c-485f-8bb8-fff43ddef114",
    "customFields": {
        "apiClient": "dotnet_sdk",
        "apiClientVersion": "0.0.1"
    }
}
```

### Отмена неоплаченного счета

Метод `cancelBill` отменяет неоплаченный счет.
В параметрах нужно указать идентификатор счета `billId` внутри вашей системы, в результате будет получен ответ с информацией о счете.
Подробнее в [документации](https://developer.qiwi.com/ru/bill-payments/#cancel).

```c#
var billId = "fcb40a23-6733-4cf3-bacf-8e425fd1fc71";

var response = client.cancelBill(billId);
```

Ответ:

```json
{
    "siteId": "270304",
    "billId": "fcb40a23-6733-4cf3-bacf-8e425fd1fc71",
    "amount": {
        "value": "199.90",
        "currency": "RUB"
    },
    "status": {
        "value": "REJECTED",
        "changedDateTime": "2018-11-03T16:03:09.062Z"
    },
    "comment": "test",
    "customer": {
        "email": "example@mail.org",
        "account": "349d5978-bccc-4e10-be7e-3ca0808237b7",
        "phone": "79123456789"
    },
    "creationDateTime": "2018-11-03T16:03:09.062Z",
    "expirationDateTime": "2018-12-18T16:03:08.668Z",
    "payUrl": "https://oplata.qiwi.com/form/?invoice_uid=b77618b4-746c-485f-8bb8-fff43ddef114",
    "customFields": {
        "apiClient": "dotnet_sdk",
        "apiClientVersion": "1.0.0"
    }
}
```

### Возврат средств

Метод `refund` производит возврат средств.
В параметрах нужно указать:
* идентификатор счета `billId`;
* идентификатор возврата `refundId` внутри вашей системы;
* сумму возврата `amount`.

Подробнее в [документации](https://developer.qiwi.com/ru/bill-payments/#refund).

```c#
var billId = "fcb40a23-6733-4cf3-bacf-8e425fd1fc71";
var refundId = Guid.NewGuid().ToString();
var amount = new MoneyAmount
{
    ValueDecimal = 104.9m,
    CurrencyEnum = CurrencyEnum.Rub
};

var refundResponse = client.refundBill(paidBillId, refundId, amount);
```

В результате будет получен ответ c информацией о возврате:

```json
{
    "amount": {
        "value": "104.90",
        "currency": "RUB"
    },
    "dateTime": "2018-11-03T16:11:57.8Z",
    "refundId": "3444e8ca-cf68-4dbd-92ee-f68c4bf8f29b",
    "status": "PARTIAL"
}
```

### Информация о возврате

Метод `getRefundInfo` запрашивает статус возврата, в параметрах нужно указать:
* идентификатор счета `billId`;
* идентификатор возврата `refundId` внутри вашей системы.

Подробнее в [документации](https://developer.qiwi.com/ru/bill-payments/#refund-status).

```c#
var billId = "fcb40a23-6733-4cf3-bacf-8e425fd1fc71";
var refundId = "3444e8ca-cf68-4dbd-92ee-f68c4bf8f29b";

var response = client.getRefundInfo(paidBillId, refundId);
```

В результате будет получен ответ c информацией о возврате:

```json
{
    "amount": {
        "value": "104.90",
        "currency": "RUB"
    },
    "dateTime": "2018-11-03T16:11:57.8Z",
    "refundId": "3444e8ca-cf68-4dbd-92ee-f68c4bf8f29b",
    "status": "PARTIAL"
}

```

### Вспомогательные методы

Метод `checkNotificationSignature` осуществляет проверку подписи при нотификации о новом счете от сервера уведомлений QIWI. Принимает на вход подпись из входящего запроса, объект - тело запроса и secret ключ, с помощью которого должна осуществляться подпись:

```c#
var merchantSecret = "test-merchant-secret-for-signature-check";
var notification = new Notification
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
};
var validSignature = "07e0ebb10916d97760c196034105d010607a6c6b7d72bfa1c3451448ac484a3b";

BillPaymentsUtils.checkNotificationSignature(validSignature, notification, merchantSecret); //true
```

### Использование альтернативного обработчика JSON

По умолчанию для работы с JSON используется `System.Runtime.Serialization.Json.DataContractJsonSerializer`, но можно воспользоваться любым другим, реализовав интерфейс `IObjectMapper`.

```c#
public interface IClient {
    string writeValue(object entityOpt);
    T readValue<T>(string body);
}
```

Примером реализации является `ContractObjectMapper`, принимающий `System.Runtime.Serialization.Json.DataContractJsonSerializerSettings` для конфигурации совместимости формата дат.

```c#
BillPaymentClient client = BillPaymentClientFactory.create(
    secretKey,
    null,
    ObjectMapperFactory.create<ContractObjectMapper>(
        new DataContractJsonSerializerSettings
        {
            DateTimeFormat = new DateTimeFormat(BillPaymentsClient.DatetimeFormat)
        }
    )
);
```

Проект **Qiwi.BillPayments.Json.Newtonsoft** предоставляет обработчик JSON на освнове пакета [Newtonsoft.Json](https://www.newtonsoft.com/json).

### Использование альтернативного HTTP-клиента

По умолчанию для отправки HTTP-запросов используется `System.Net.Http.HttpClient`, но можно воспользоваться любым другим, реализовав интерфейс `IClient`.

```c#
public interface IClient {
    ResponseData request(
        string method,
        string url,
        IReadOnlyDictionary<string, string> headers,
        string entityOpt = null
    );
}
```

Примером реализации является `NetClient`, принимающий `System.Net.Http.HttpClient` произвольной конфигурации.

```c#
BillPaymentClient client = BillPaymentClientFactory.create(
    secretKey,
    ClientFactory.create<NetClient>(new HttpClient())
);
```

## Требования

* **.Net Standart 2.0** или **.Net Core 2.0** или **.Net Framework 4.5**

## Лицензия

[MIT](LICENSE)