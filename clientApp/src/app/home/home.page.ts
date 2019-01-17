import { Component, OnInit } from '@angular/core';
import { HomeService } from './home.service';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit {
  dash: any;
  constructor(private homeService: HomeService) { }

  ngOnInit(): void {
    this.homeService.getDashboard()
      .subscribe(res => {
        console.log(res);
        this.dash = res;
      });
  }
}
