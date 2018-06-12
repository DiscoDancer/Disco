export default class ApiService {
    static addLog(activityId) {
        return fetch("/Timer/AddLog",
            {
                method: "POST",
                body: { ActivityId: activityId }
            });
    }
}