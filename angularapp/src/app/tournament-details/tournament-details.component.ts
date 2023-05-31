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
    constructor(private activatedRoute: ActivatedRoute,private client: Client) { }

    ngOnInit(): void {
        this.activatedRoute.params.subscribe(params => {
            this.id = params['id'];
            console.log(this.id);
        });
        this.loadData();
    }


    loadData(): void {
        this.client.tournaments(parseInt(this.id)).subscribe(res => {
            this.tournament = res;
            this.tournament.gameIcon = "https://localhost:7010/" + this.tournament.gameIcon;
            this.newName = this.tournament.name!;
            this.client.applications2(this.tournament.id!).subscribe(res => {
                this.applications = res;
            })
            this.client.my(undefined, undefined, this.tournament.gameId).subscribe(res => {
                this.teams = res;
            })
            this.client.applications2(this.tournament.id!).subscribe(res => {
                this.applications = res;
            })
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

    join (id: number) {
        this.client.apply2(id, this.tournament.id!).subscribe(res => {
            this.loadData();
        })
    }


}
