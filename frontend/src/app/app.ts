import { Component } from '@angular/core';
import { UploadForm } from './components/upload-form/upload-form';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [UploadForm],
  templateUrl: './app.html',
  styleUrls: ['./app.scss']
})
export class App {
  title = 'file-processor-web';
}
