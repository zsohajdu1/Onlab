import { Component } from '@angular/core';
import { Client, CreateTeamDTO, GetGameDTO } from '../client';
import { Router } from '@angular/router';

@Component({
    selector: 'app-create-team',
    templateUrl: './create-team.component.html',
})
export class CreateTeamComponent {
    games: GetGameDTO[] = [];
    name: string;
    description: string = "";
    selectedGame: GetGameDTO;
    oldgameId: number;
    viewGame: string = "Select Game";
    alertMessage: string = "";
    alert: boolean = false;
    constructor(private client: Client, private router: Router) {
        this.client.games().subscribe(res => {
            this.games = res;
        })
    }

    changeGameId(game: GetGameDTO | undefined): void {
        if (game?.name) {
            this.selectedGame = game;
            this.viewGame = game.name;
        }
    }

    createTeam(): void {
        if (!this.name) {
            this.alert = true;
            this.alertMessage = "Enter a name!";
        }
        else if (!this.selectedGame) {
            this.alert = true;
            this.alertMessage = "Select a game!";
        }
        else {
            this.client.create2(new CreateTeamDTO({ name: this.name, teamGameId: parseInt(this.selectedGame.id!), lftDescription: this.description })).subscribe(res => {
                this.router.navigate(['/teams', res]);
            })
        }
    }
}
