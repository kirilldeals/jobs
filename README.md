# REST API для игры в крестики нолики
## Использованные технологии
+ ASP.NET Core
+ База данных MongoDB
+ MongoDB.Driver драйвер .NET для MongoDB

## Входные и выходные данные
### Параметры ответа
| Параметр | Тип | Описание |
|---|---|---|
| id | `string` | Идентификатор игры |
| field | `string[]` | Игровое поле 3х3 |
| currentPlayer | `enum` | Текущий игрок который должен сделать ход (первым ходит игрок **'X'**) |
| gameState | `enum` | Состояние игры на текущий момент |

#### currentPlayer
| Значение | Описание |
|:---:|---|
0 | Нет. Значение устанавливается для завершенных игр
1 | Игрок **'X'**. Значение по умолчанию для нового экземпляра игры
2 | Игрок **'O'**
#### gameState
| Значение | Описание |
|:---:|---|
0 | В процессе. Значение по умолчанию для нового экземпляра игры
1 | Победа игрока **'X'**
2 | Победа игрока **'O'**
3 | Ничья

### Параметры запроса
| Параметр | Тип | Описание |
|---|---|---|
| row | `int` | Номер строки игрового поля от 1 до 3 |
| col | `int` | Номер столбца игрового поля от 1 до 3 |

## Методы

### Получить все игры
```http
GET /api/Games
```
#### Пример ответа
```json
[
  {
    "id": "64163702fb3b128ba4f54aad",
    "field": [
      "...",
      "...",
      "..."
    ],
    "currentPlayer": 1,
    "gameState": 0
  },
  {
    "id": "64163702fb3b128ba4f54aae",
    "field": [
      "OOO",
      ".XX",
      "..X"
    ],
    "currentPlayer": 0,
    "gameState": 2
  }
]
```
____
### Получить все игры с определенным состоянием
```http
GET /api/Games/getByGameState?state={state}
```
#### Пример ответа
```json
[
  {
    "id": "64163702fb3b128ba4f54aad",
    "field": [
      "...",
      "...",
      "..."
    ],
    "currentPlayer": 1,
    "gameState": 0
  }
]
```
**Request URL: /api/Games/getByGameState?state=0**
____
### Получить игру по идентификатору
```http
GET /api/Games/{id}
```
#### Пример ответа
```json
{
  "id": "64163702fb3b128ba4f54aad",
  "field": [
    "...",
    "...",
    "..."
  ],
  "currentPlayer": 1,
  "gameState": 0
}
```
**Request URL: /api/Games/64163702fb3b128ba4f54aad**
____
### Создать игру
```http
POST /api/Games/create
```
#### Пример ответа
```json
{
  "id": "64170d3db228f0ba739dce4a",
  "field": [
    "...",
    "...",
    "..."
  ],
  "currentPlayer": 1,
  "gameState": 0
}
```
____
### Удалить игру
```http
DELETE /api/Games/delete/{id}
```
#### Пример ответа
```json
{
  "id": "64163702fb3b128ba4f54aae",
  "field": [
    "OOO",
    ".XX",
    "..X"
  ],
  "currentPlayer": 0,
  "gameState": 2
}
```
____
### Сделать ход
```http
PUT /api/Games/{id}?row={row}&col={col}
```
#### Пример успешного ответа
```json
{
  "id": "64163702fb3b128ba4f54aad",
  "field": [
    ".X.",
    "...",
    "..."
  ],
  "currentPlayer": 2,
  "gameState": 0
}
```
**Request URL: /api/Games/64163702fb3b128ba4f54aad?row=1&col=2**
#### Пример ответа при попытке сделать ход в заполненной клетке
```json
Player "X" already made a move in the cell (1,2)
```
#### Пример ответа при попытке сделать ход в завершенной игре
```json
The game has been finished
```
