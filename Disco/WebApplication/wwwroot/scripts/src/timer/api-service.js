export default class ApiService {
    static addLog(activityId) {
        return fetch("/Timer/AddLog",
            {
                method: "POST",
                body: JSON.stringify({ activityId }),
                headers: {
                    'content-type': 'application/json'
                }
            });
    }
}