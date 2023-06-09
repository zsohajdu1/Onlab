import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { TeamsComponent } from './teams/teams.component';
import { environment } from '../environments/environment';
import { ApiAuthorizationModule } from './api-authorization/api-authorization.module';
import { AuthorizeGuard } from './api-authorization/authorize.guard';
import { AuthorizeInterceptor } from './api-authorization/authorize.interceptor';
import { Client } from './client';
import { MyTeamsComponent } from './my-teams/my-teams.component';
import { TournamentsComponent } from './tournaments/tournaments.component';
import { MyTournamentsComponent } from './my-tournaments/my-tournaments.component';
import { TournamentDetailsComponent } from './tournament-details/tournament-details.component';
import { TeamDetailsComponent } from './team-details/team-details.component';
import { CreateTeamComponent } from './create-team/create-team.component';
import { CreateTournamentComponent } from './create-tournament/create-tournament.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    TeamsComponent,
    HomeComponent,
    MyTeamsComponent,
    TournamentsComponent,
    MyTournamentsComponent,
    TournamentDetailsComponent,
    TeamDetailsComponent,
    CreateTeamComponent,
    CreateTournamentComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule,
    ApiAuthorizationModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'teams', component: TeamsComponent, pathMatch: 'full' },
      { path: 'myteams', component: MyTeamsComponent, pathMatch: 'full', canActivate: [AuthorizeGuard] },
      { path: 'teams/create', component: CreateTeamComponent, pathMatch: 'full', canActivate: [AuthorizeGuard] },
      { path: 'tournaments', component: TournamentsComponent, pathMatch: 'full' },
      { path: 'tournaments/create', component: CreateTournamentComponent, pathMatch: 'full', canActivate: [AuthorizeGuard] },
      { path: 'tournaments/:id', component: TournamentDetailsComponent, pathMatch: 'full', canActivate: [AuthorizeGuard] },
      { path: 'mytournaments', component: MyTournamentsComponent, pathMatch: 'full', canActivate: [AuthorizeGuard] },
      { path: "tournaments/:id", component: TournamentDetailsComponent, pathMatch: 'full', canActivate: [AuthorizeGuard] },
      { path: "teams/:id", component: TeamDetailsComponent, pathMatch: 'full', canActivate: [AuthorizeGuard] }
    ])
  ],

  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    Client
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
