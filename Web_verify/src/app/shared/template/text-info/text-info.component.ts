import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-text-info',
  templateUrl: './text-info.component.html',
  styleUrls: ['./text-info.component.scss']
})
export class TextInfoComponent implements OnInit {

  @Input() label?: string;
  @Input() value?: string | number;
  @Input() typeValue?: string;
  @Input() typeShowHtml?: boolean;

  constructor(
  ) {}

  ngOnInit() {
  }
}
