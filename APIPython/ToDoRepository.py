from datetime import datetime, timedelta

class ToDoRepository:
    def __init__(self):
        self.toDoList = []
    
    def AddToDos(self, toDoToAdd):
        self.toDoList.append(toDoToAdd)

    def PrintToDoList(self):
        for todo in self.toDoList:
            print("Title: " + todo.title + "\nStart: " + todo.startDt + "\nEnd: " + todo.endDt)

    def DailyTasksInWeek(self):
        listDailyTasks = {}
        for todo in self.toDoList:
            date = todo.startDt.date()
            if date >= datetime.now().date() and date < (datetime.now().date() + timedelta(days=7)):
                #print(date)
                if date not in listDailyTasks:
                    listDailyTasks[date] = 0
                listDailyTasks[date] += 1
        
        res = [{"date": date.strftime("%Y-%m-%d"), "amount": amount} for date, amount in listDailyTasks.items()]
        #print(res)
        #limpando a lista para nao acumular 
        self.toDoList.clear()
        return res
    
    def TotalDays(self):
        totalDays = 0
        for todo in self.toDoList:
            duration = todo.startDt.date() - todo.endDt.date()
            totalDays += duration.days
        self.toDoList.clear()
        res = {"totalDays": totalDays}
        return res
    