import { Component, OnInit } from '@angular/core';
import { Client, TeamListDTO, TournamentApplicationDTO, TournamentDetailDTO } from '../client';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-tourmament-details',
    templateUrl: './tournament-details.component.html',
})
export class TournamentDetailsComponent implements OnInit {
    tournament: TournamentDetailDTO;
    id: string;
    newName: string;
    applications: TournamentApplicationDTO[];
    teams: TeamListDTO[];
    newParticipants: number;
    alert: boolean = false;
    alertMessage: string = "";
    constructor(private activatedRoute: ActivatedRoute, private client: Client) { }

    ngOnInit(): void {
        this.activatedRoute.params.subscribe(params => {
            this.id = params['id'];
        });
        this.loadData();
    }


    loadData(): void {
        this.client.tournaments(parseInt(this.id)).subscribe(res => {
            this.tournament = res;
            this.tournament.gameIcon = "https://localhost:7010/" + this.tournament.gameIcon;
            this.newName = this.tournament.name!;
            this.newParticipants = this.tournament.maxParticipants!;
            this.client.applications2(this.tournament.id!).subscribe(res => {
                this.applications = res;
            })
            this.client.my(undefined, undefined, this.tournament.gameId).subscribe(res => {
                this.teams = res;
            })
            this.client.applications2(this.tournament.id!).subscribe(res => {
                this.applications = res;
            })
            console.log(this.tournament.matches);
        })
    }

    accept(id: number) {
        this.client.acceptPOST(this.tournament.id!, id).subscribe(res => {
            this.loadData();
        })
    }

    deny(id: number) {
        this.client.denyPOST(this.tournament.id!, id).subscribe(res => {
            this.loadData();
        })
    }

    remove(id: number) {
        this.client.remove2(this.tournament.id!, id).subscribe(res => {
            this.loadData();
        })
    }

    join(id: number) {
        this.client.apply2(id, this.tournament.id!).subscribe(res => {
            this.loadData();
        })
    }

    save() {

        if (!(this.newParticipants == 2
            || this.newParticipants == 4
            || this.newParticipants == 8
            || this.newParticipants == 16)) {
            this.alert = true;
            this.alertMessage = "Enter a valid participant number (2, 4, 8, 16)!";
        }
        else if (this.newParticipants < this.tournament.teamsName!.length) {
            this.alert = true;
            this.alertMessage = "Please enter a number, which is less than the current number of teams: "
                + this.tournament.teamsName?.length.toString()
                + "!";
        }
        else {
            this.alert = false;
            this.tournament.name = this.newName;
            this.tournament.maxParticipants = this.newParticipants;
            this.client.change2(this.tournament).subscribe(_ => {
                this.loadData();
            });
        }
    }

    start(): void {
        this.client.start(this.tournament.id!).subscribe(res => {
            this.loadData();
        })
    }

    winner(matchId: number, winnerId: number): void {
        this.client.matches(matchId, winnerId).subscribe(res => {
            this.loadData();
        });
    }
}
