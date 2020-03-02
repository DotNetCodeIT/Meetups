import { check, group, sleep } from "k6";
import http from "k6/http";

// User scenario
export default function() {
    group("Get Food", function() {
        let res = http.get("https://meetup2020.azurewebsites.net/api/v1/foods");

        check(res, {
            "is status 200": (r) => r.status === 200
        });

        sleep(5);
    });
}