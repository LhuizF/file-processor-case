import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ProcessedFile } from './models/processed-file.model';
import { ApiService } from './services/api.service';
import { UploadForm } from './components/upload-form/upload-form';
import { FileList } from './components/file-list/file-list';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, UploadForm, FileList, CommonModule],
  templateUrl: './app.html',
  styleUrls: ['./app.scss']
})
export class App implements OnInit {
  processedFiles$!: Observable<ProcessedFile[]>;

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.loadFiles();
  }

  loadFiles(): void {
    this.processedFiles$ = this.apiService.getProcessedFiles();
  }
}
