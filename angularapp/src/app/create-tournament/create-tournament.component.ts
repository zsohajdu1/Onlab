import { Component } from '@angular/core';
import { Client, CreateTournamentDTO, GetGameDTO } from '../client';
import { Router } from '@angular/router';

@Component({
    selector: 'app-create-tournament',
    templateUrl: './create-tournament.component.html',
})
export class CreateTournamentComponent {
    games: GetGameDTO[] = [];
    name: string;
    selectedGame: GetGameDTO;
    oldgameId: number;
    viewGame: string = "Select Game";
    alertMessage: string = "";
    alert: boolean = false;
    participants: string = "";
    parsedParticipants: number;
    constructor(private client: Client, private router: Router) {
        this.client.games().subscribe(res => {
            this.games = res;
        })
    }

    changeGameId(game: GetGameDTO | undefined): void {
        if (game && game.name) {
            this.selectedGame = game;
            this.viewGame = game.name;
        }
    }

    createTournament(): void {
        this.parsedParticipants = parseInt(this.participants);
        if (!this.name) {
            this.alert = true;
            this.alertMessage = "Enter a name!";
        }
        else if (!(this.parsedParticipants == 2
            || this.parsedParticipants == 4
            || this.parsedParticipants == 8
            || this.parsedParticipants == 16)) {
            this.alert = true;
            this.alertMessage = "Enter a valid participant number (2, 4, 8, 16)!";
        }
        else if (!this.selectedGame) {
            this.alert = true;
            this.alertMessage = "Select a game!";
        }
        else {
            this.client.create3(new CreateTournamentDTO({
                name: this.name,
                maxParticipants: this.parsedParticipants,
                gameId: parseInt(this.selectedGame.id!)
            })).subscribe(res => {
                this.router.navigate(['/tournaments', res]);
            })
        }
    }
}
