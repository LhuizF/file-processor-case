import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ProcessedFile } from './models/processed-file.model';
import { ApiService } from './services/api.service';
import { UploadForm } from './components/upload-form/upload-form';
import { SummaryChart } from './components/summary-chart/summary-chart';
import { FileList } from './components/file-list/file-list';
import { StatsModel } from './models/stats.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [UploadForm, FileList, CommonModule, SummaryChart],
  templateUrl: './app.html',
  styleUrls: ['./app.scss']
})
export class App implements OnInit {
  processedFiles$!: Observable<ProcessedFile[]>;
  stats$!: Observable<StatsModel>;
  isLoading = false;

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.loadData();
  }

  async loadData(): Promise<void> {
    this.isLoading = true;
    await new Promise(resolve => setTimeout(resolve, 500));
    this.processedFiles$ = this.apiService.getProcessedFiles();
    this.stats$ = this.apiService.getStats();
    this.isLoading = false;
  }
}
