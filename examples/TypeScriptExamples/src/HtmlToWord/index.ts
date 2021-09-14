import { join } from 'path';
import * as fs from 'fs';
import { JSDOM } from 'jsdom';
import { loadImage, createCanvas } from 'canvas';
import { asBlob } from 'html-docx-js-typescript';
// import * as HTMLtoDOCX from 'html-to-docx';

const basePath = process.cwd().replace('dist', 'src');
const output = process.cwd();

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
      max-width: 100%;
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

const targetWidth = 595;
const targetHeight = 842;

function imgZoom(
  imgWidth: number,
  imgHeight: number,
): { width: number; height: number } {
  let width: number;
  let height: number;

  if (imgWidth > 0 && imgHeight > 0) {
    //原图片宽高比例 大于 指定的宽高比例，这就说明了原图片的宽度必然 > 高度
    if (imgWidth / imgHeight >= targetWidth / targetHeight) {
      if (imgWidth > targetWidth) {
        width = targetWidth;
        // 按原图片的比例进行缩放
        height = (imgHeight * targetWidth) / imgWidth;
      } else {
        // 按照图片的大小进行缩放
        width = imgWidth;
        height = imgHeight;
      }
    } else {
      // 原图片的高度必然 > 宽度
      if (imgHeight > targetHeight) {
        height = targetHeight;
        // 按原图片的比例进行缩放
        width = (imgWidth * targetHeight) / imgHeight;
      } else {
        // 按原图片的大小进行缩放
        width = imgWidth;
        height = imgHeight;
      }
    }
  }

  return { width, height };
}

async function saveDocx(): Promise<void> {
  const htmlString = fs.readFileSync(join(basePath, 'assets', 'content.html'), {
    encoding: 'utf-8',
  });

  console.log(htmlString);

  const dom = new JSDOM(htmlString);
  const doc = dom.window.document;

  console.log('Converting image to base64');

  for (const img of doc.querySelectorAll('img')) {
    const imgPath = join(basePath, img.src);

    const svg = await loadImage(imgPath);
    console.log(`The image is width: ${svg.width} height: ${svg.height}`);

    if (svg.width > targetWidth || svg.height > targetHeight) {
      const { width, height } = imgZoom(svg.width, svg.height);

      console.log(
        `The image exceeds the specified width and height. Being zoomed, width: ${width}, height: ${height}`,
      );

      const canvas = createCanvas(width, height);
      const context = canvas.getContext('2d');
      context.drawImage(svg, 0, 0, width, height);

      img.src = canvas.toDataURL('image/png');
    } else {
      const imgBase64 = fs.readFileSync(imgPath, { encoding: 'base64' });
      img.src = `data:image/png;base64,${imgBase64}`;
    }
  }

  const html = htmlTemplate.replace('{htmlContent}', doc.body.innerHTML);

  const data = await asBlob(html);
  // const data = await HTMLtoDOCX(html, null, {
  //   table: { row: { cantSplit: true } },
  // });
  const filename = join(output, 'file.docx');
  fs.writeFileSync(filename, data as Buffer);
}

saveDocx().then();
