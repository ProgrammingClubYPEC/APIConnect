# Примитивный пример создания GET и POST запроса к API на С#


## **Данные по API** 


**Начальный URL к API: mskko2021.mad.hakta.pro/api**


## Методы к API
| **Название метода**  | **Описание**                                                                                                                                                                                                                                                                                                   | **Принимаемые значения**       | **Ответ**                                                                                 |
|----------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--------------------------------|-------------------------------------------------------------------------------------------|
| **GET /quotes**      | Возвращает список цитат                                                                                                                                                                                                                                                                                        | -                              | { "success": true, "data": [ { "id": 0, "title": "", "image": "", "description": “" }, ]} |
| **GET /feelings**    | Возвращает список ощущений                                                                                                                                                                                                                                                                                     | -                              | { "success": true, "data": [ { "id": 0, "title": "", "image": “", “position”: 0 }, ] }    |
| **POST /user/login** | Позволяет получить данные пользователя и token авторизации. Данные отправляются в POST Body. Content -Type: application/ json  **Для тестирования авторизации используйте одну из учетных записей:**  Login: junior@wsr.ru Password: junior  Login: general@wsr.ru Password: general  Login: wsr Password: wsr | { "email": "", "password":"" } | { "id": "", "email": "", "nickName": “”, "avatar": “", "token": "" }                      |