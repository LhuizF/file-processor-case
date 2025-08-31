import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Stats } from '../models/stats.model';
import { ProcessedFile } from '../models/processed-file.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private readonly baseUrl = 'http://localhost:5219/api';

  constructor(private http: HttpClient) {}

  getStats(): Observable<Stats> {
    return this.http.get<Stats>(`${this.baseUrl}/processedFiles/status`);
  }

  getProcessedFiles(): Observable<ProcessedFile[]> {
    return this.http.get<ProcessedFile[]>(`${this.baseUrl}/processedFiles`);
  }

  uploadFile(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post<any>(`${this.baseUrl}/acquirerFiles`, formData);
  }
}
