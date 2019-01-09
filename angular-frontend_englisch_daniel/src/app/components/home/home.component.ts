import { Component, OnInit } from '@angular/core';
import { Station } from 'src/app/services/DTOs/station';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'wetr-home',
  templateUrl: './home.component.html',
  styles: []
})
export class HomeComponent implements OnInit {

  stations : Array<Station>

  constructor(private api : ApiService) { }

  async ngOnInit() {
    this.stations = await this.api.getStations()
    console.log(this.stations)
  }

}
