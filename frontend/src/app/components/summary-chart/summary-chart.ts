import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StatsModel } from '../../models/stats.model';

@Component({
  selector: 'app-summary-chart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './summary-chart.html',
  styleUrls: ['./summary-chart.scss']
})
export class SummaryChart implements OnChanges {
  @Input() stats: StatsModel | null = null;

  gradientStyle: string = '';

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['stats'] && this.stats) {
      this.updateChart();
    }
  }

  private updateChart(): void {
    if (!this.stats || this.stats.totalFiles === 0) {
      this.gradientStyle = 'conic-gradient(#eee 0% 100%)';
      return;
    }

    // 👇 LÓGICA SIMPLIFICADA: Pegamos a porcentagem de sucesso direto do objeto 'stats'
    const successPercentage = this.stats.success.percent;

    const successColor = '#2e7d32';
    const failureColor = '#c62828';

    // A montagem do gradiente continua a mesma, mas agora com o valor vindo da API
    this.gradientStyle = `conic-gradient(
      ${successColor} 0% ${successPercentage}%,
      ${failureColor} ${successPercentage}% 100%
    )`;
  }
}
