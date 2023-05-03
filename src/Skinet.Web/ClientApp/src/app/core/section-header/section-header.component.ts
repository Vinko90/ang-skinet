import { Component } from '@angular/core';
import {BreadcrumbService} from "xng-breadcrumb";

@Component({
  selector: 'app-section-header',
  templateUrl: './section-header.component.html'
})
export class SectionHeaderComponent {
  constructor(public bcService: BreadcrumbService) { }
}
