import { Component } from '@angular/core';

@Component({
  selector: 'app-css-report',
  templateUrl: './css-report.component.html',
  styleUrls: ['./css-report.component.scss']
})
export class CssReportComponent {
  ngOnInit(): void {
    if (window.location.hash === "#list-mailtab-2") {
      // Find the tab element by its ID
      var tabElement = document.getElementById("list-mailtab-2");

      // Check if the tab element exists
      if (tabElement) {
        // Trigger a click event on the tab element
        tabElement.click();
      }
    }




  }
}
