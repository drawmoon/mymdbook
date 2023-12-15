from fastapi import FastAPI


app = FastAPI(title='Simple API')


@app.get('/users')
def getAll():
    return [ 'Randolph', 'Jesse' ]


@app.get('/users/{id}')
def getById(id: int):
    return 'Randolph'


@app.post('/users')
def post(user: str):
    return user


@app.put('/users/{id}')
def put(id: int, user: str):
    return user


@app.delete('/users/{id}')
def delete(id: int):
    return None
