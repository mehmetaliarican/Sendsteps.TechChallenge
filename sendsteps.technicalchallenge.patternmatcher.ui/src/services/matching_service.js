

export class MatchingResult {
    icon = "";
    primary = "";
    secondary = "";
    value = "";
    expected = "";

    constructor(obj) {
        Object.assign(this, obj);
        if (obj.isOverlapping) {
            this.icon = this.convertUnicode("\u2705");
        }
        else {
            this.icon = this.convertUnicode("\u274C");
        }
    }

    convertUnicode(input) {
        return input.replace(/\\+u([0-9a-fA-F]{4})/g, (a, b) =>
            String.fromCharCode(parseInt(b, 16)));
    }
}

export class MatchingService {

    async fetchResultsAsync(requestBody) {
        let matchingResults = [];
        let hasError = false;
        let promise = undefined;

        await fetch("http://localhost:5001/patternmatching",
            {
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(requestBody)
            })
            .then(res => res.json())
            .then(d => {
                if (d.success) {
                    console.log(d.data);
                    matchingResults.push( new MatchingResult(d.data));
                }
            })
            .catch(error => {
                console.error("Fetch error", error);
                hasError = true;
            })
            .finally(() => {
                promise = new Promise((resolve) => {
                    if (!hasError) {
                        resolve({
                            data: matchingResults,
                            message: matchingResults.length > 0 ? "" : "No result"
                        });
                    }
                    else {
                        resolve({
                            data: [],
                            message: "Try later, a server error."
                        });
                    }
                });
            });
            return promise;
    }
}