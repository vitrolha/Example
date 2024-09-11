from flask import Flask, jsonify
import requests, json, ToDo, ToDoRepository
from flask_cors import CORS

app = Flask(__name__)

#Cors config
CORS(app, resources={r"/*": {"origins":"https://localhost:7266"}})

toDoRepository = ToDoRepository.ToDoRepository()

@app.route('/daily-tasks', methods=['GET'])
def DailyTasksInWeek():
    data = requests.get('https://localhost:7156/api/ToDo/getall', verify=False)

    if data.status_code == 200:
        todoList = json.loads(data.text)
        for todo in todoList:
            task = ToDo.ToDo(todo['title'], todo['start'], todo['end'])
            toDoRepository.AddToDos(task)
        #print("Printando a lista de ToDos feitas\n")
        #toDoRepository.PrintToDoList()
        #toDoRepository.DailyTasks()
        #print("\n")
        return jsonify(toDoRepository.DailyTasksInWeek())

@app.route('/total-days', methods=['GET'])
def TotalDays():
    data = requests.get('https://localhost:7156/api/ToDo/getall', verify=False)
    if data.status_code == 200:
        todoList = json.loads(data.text)
        for todo in todoList:
            task = ToDo.ToDo(todo['title'], todo['start'], todo['end'])
            toDoRepository.AddToDos(task)
        
        return jsonify(toDoRepository.TotalDays())

if __name__ == '__main__':
    app.run(debug=True)