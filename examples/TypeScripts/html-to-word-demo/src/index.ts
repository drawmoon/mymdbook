import { asBlob } from 'html-docx-js-typescript';
import * as path from 'path';
import * as fs from 'fs';
import { JSDOM } from 'jsdom';
// import { loadImage, createCanvas } from 'canvas';

const htmlTemplate = `
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <title>Document</title>
  <style>
    table {
      min-height: 25px;
      line-height: 25px;
      text-align: center;
      border-collapse: collapse;
    }
    table,
    table tr th,
    table tr td {
      border: 1px solid #a8aeb2;
      padding: 5px 10px;
    }
    img {
      max-width: 1600px;
      max-height: 100%;
    }
    figure.image {
      display: table;
      clear: both;
      text-align: center;
      margin: 1em auto;
    }
  </style>
</head>
<body>
  {htmlContent}
</body>
</html>`;

async function saveDocx(): Promise<void> {
  const htmlString = fs.readFileSync(
    path.join(process.cwd(), 'assets', 'content.html'),
    {
      encoding: 'utf-8',
    },
  );

  const dom = new JSDOM(htmlString);
  const doc = dom.window.document;

  // const canvas = doc.createElement('canvas');
  // const ctx = canvas.getContext('2d');

  // 将图片转换为 base64
  for (const img of doc.querySelectorAll('img')) {
    const imgPath = path.join(process.cwd(), img.src);

    const imgBase64 = fs.readFileSync(imgPath, { encoding: 'base64' });
    img.src = `data:image/png;base64,${imgBase64}`;

    // img.src = imgPath;
    // // preparing canvas for drawing
    // ctx.clearRect(0, 0, canvas.width, canvas.height);
    // canvas.width = img.width;
    // canvas.height = img.height;

    // ctx.drawImage(img, 0, 0);
    // // by default toDataURL() produces png image, but you can also export to jpeg
    // // checkout function's documentation for more details
    // const dataURL = canvas.toDataURL();
    // // img.setAttribute('src', dataURL);
    // img.src = dataURL;

    // const imgBuffer = fs.readFileSync(imgPath);
    // const svg = await loadImage(imgBuffer);
    // // const canvas = createCanvas(img.width, img.height);
    // const canvas = createCanvas(500, 500);
    // const context = canvas.getContext('2d');
    // context.drawImage(svg, 0, 0);
    // img.src = canvas.toDataURL('image/png');
  }

  // canvas.remove();

  const html = htmlTemplate.replace('{htmlContent}', doc.body.innerHTML);

  const data = await asBlob(html, { orientation: 'landscape' });
  const filename = path.join(process.cwd(), 'dist', 'file.docx');
  fs.writeFileSync(filename, data as Buffer);
}

saveDocx().then();
