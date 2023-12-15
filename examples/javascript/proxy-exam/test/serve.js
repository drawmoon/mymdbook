const express = require('express');

const app = express();

app.get('/', (req, res) => {
  res.send('Holle, World!');
});

const port = 8000;
app.listen(port, () => console.log('Now listening on: http://localhost:%d.', port));
