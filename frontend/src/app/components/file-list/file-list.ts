import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProcessedFile } from '../../models/processed-file.model';

@Component({
  selector: 'app-file-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './file-list.html',
  styleUrls: ['./file-list.scss']
})
export class FileList {
  @Input() files: ProcessedFile[] = [];
}
