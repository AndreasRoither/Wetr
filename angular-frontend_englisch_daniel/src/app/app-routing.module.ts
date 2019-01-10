import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { EditStationComponent } from './components/edit-station/edit-station.component';


const routes: Routes = [
  { path: "", component: HomeComponent},
  { path: "login", component: LoginComponent},
  { path: "edit/:id", component: EditStationComponent},



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
export const routingComponents = [LoginComponent, HomeComponent, EditStationComponent]