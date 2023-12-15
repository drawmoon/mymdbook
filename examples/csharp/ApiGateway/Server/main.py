from fastapi import FastAPI


app = FastAPI(title='Simple API')

@app.get('/api/baskets')
def get_all():
    return [ 'value1', 'value2' ]


@app.get('/api/catalogs')
def get_all():
    return [ 'value3', 'value4' ]
