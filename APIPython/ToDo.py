from dateutil import parser

class ToDo:
    def __init__(self, title, startDt, endDt):
        self.title = title
        self.startDt = parser.parse(startDt)
        self.endDt = parser.parse(endDt)
        